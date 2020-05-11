using System.Collections;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public static EffectController instance;
    float time = 0.6f;
    SpriteRenderer sr;
    Vector3 scale;

    void Start()
    {
        instance = this;
        //transform.localScale 
        sr = GetComponent<SpriteRenderer>();
        scale = transform.localScale;
        time = 0.6f;
       
    }
    private void Update()
    {

    }
    public void changeSprite(Sprite sprite)
    {
        transform.localScale = scale * 1.3f;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(-4, 4));
        sr.sprite = sprite;
        StartCoroutine(gradeAnimation());
        time = 0;
    }
    IEnumerator gradeAnimation()
    {
        while (transform.localScale.x > scale.x)
        { 
            transform.localScale -= scale / 40.0f;
            yield return new WaitForSeconds(0.01f);
        }      
        yield break;
    }
}
