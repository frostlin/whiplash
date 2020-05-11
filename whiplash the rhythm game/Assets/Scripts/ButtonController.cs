using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite defaultImage;
    public Sprite pressedImage;
    [SerializeField]
    private AudioClip hit;
    private bool flag = false;

    public KeyCode key;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = hit;
    }

    float alpha = 0.05f;
    void Update()
    {
        if (GameController.instance.gamePaused) return;
        if (Input.GetKeyDown(key))
        {
            if (!flag)
            {
                GetComponent<AudioSource>().Play();
                flag = true;
            }
            sr.sprite = pressedImage;                
        }
 
                

        if (Input.GetKeyUp(key))
        {
            flag = false;
        }
        if (flag && sr.color.a < 1)
            sr.color = new Color(1f, 1f, 1f, sr.color.a + alpha);
        if (!flag && sr.color.a > alpha * 3)
            sr.color = new Color(1f, 1f, 1f, sr.color.a - alpha / 2);
        if (sr.color.a <= alpha * 3) 
            sr.sprite = defaultImage;
    }

}
