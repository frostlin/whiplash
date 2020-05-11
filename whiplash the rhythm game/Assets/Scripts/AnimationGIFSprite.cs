using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationGIFSprite : MonoBehaviour
{
    public Sprite[] animatedImages;
    public Image animatedImageObj;
    public int fps = 24;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //animatedImageObj.sprite = animatedImages[(int)(Time.time*fps) % animatedImages.Length];
    }
}
