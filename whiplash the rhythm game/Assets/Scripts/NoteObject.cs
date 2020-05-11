using UnityEngine;


public class NoteObject : MonoBehaviour
{
    private bool canBePressed;
    public bool wasHit = false;
    bool flag = true;
    float colliderSizeY;
    float colliderCenterY;
    KeyCode key;

    void Update()
    {
        if (GameController.instance.gamePaused) return;
        if (Input.GetKeyDown(key))
        {
            if (canBePressed && flag)
            {
                wasHit = true;
                flag = false;
                float magnitude = Mathf.Abs(transform.position.y - colliderCenterY) / colliderCenterY;
                GameController.instance.noteHit(magnitude, transform.position);
                gameObject.SetActive(false);
            }
        }
        else flag = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Button"))
        {
            key = other.GetComponent<ButtonController>().key;
            canBePressed = true;
            colliderSizeY = other.GetComponent<BoxCollider2D>().size.y / 2;
            colliderCenterY = other.GetComponent<BoxCollider2D>().offset.y - other.transform.position.y;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canBePressed = false;
        if (other.tag.Equals("Button") && !wasHit)
        {
            GameController.instance.noteMissed();
        }
    }
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}
