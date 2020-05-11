using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SliderController : MonoBehaviour
{
    public float length;
    [SerializeField]
    private GameObject startNote;
    [SerializeField]
    private GameObject finishNote;
    [SerializeField]
    private GameObject filler;
    [SerializeField]
    private GameObject hitEffect;

    KeyCode key;

    bool canBePressed;
    bool isHolding;
    bool missed;

    float colliderCenterY;
    float speed;
    float timer = 0.3f;
   
    Vector3 hitPosition;
    Vector2 fillerSize;
  
    void Start()
    {
        length = finishNote.transform.position.y - startNote.transform.position.y;
        fillerSize = filler.GetComponent<SpriteRenderer>().size;
        speed = 1;

        filler.transform.position = new Vector3(
            startNote.transform.position.x, startNote.transform.position.y + length / 2, 0);
        filler.transform.localScale = new Vector3(1, length / 2, 1);

        GetComponent<BoxCollider2D>().size = 
            new Vector2(0.8f, length / 2 + startNote.GetComponent<SpriteRenderer>().size.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
        {
            if (canBePressed && !missed)
            {
                isHolding = true;
                if (startNote != null)
                {
                    float magnitude = Mathf.Abs(startNote.transform.position.y - colliderCenterY) / colliderCenterY;
                    GameController.instance.noteHit(magnitude, startNote.transform.position);
                    hitPosition = startNote.transform.position;
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        Instantiate(hitEffect, hitPosition, Quaternion.identity);
                        timer = 0.3f;   
                    }
                    Destroy(startNote);
                } else
                {
                    //filler.transform.position = new Vector3(filler.transform.position.x,
                    //    speed * Time.deltaTime / 120,
                    //    filler.transform.position.z);
                    //
                    //filler.transform.localScale = new Vector3(1, ,1)
                    //filler.GetComponent<SpriteRenderer>().size = new Vector2(fillerSize.x, speed * Time.deltaTime / 120);
                }
            }
        }
        else if (isHolding)
        {
            missed = true;
            isHolding = false;
            GameController.instance.noteMissed();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Button"))
        {
            canBePressed = true;
            key = collision.GetComponent<ButtonController>().key;
            colliderCenterY = collision.GetComponent<BoxCollider2D>().offset.y - collision.transform.position.y;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Button"))
        {
            
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
