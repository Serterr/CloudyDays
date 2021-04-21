using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DogBowl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public RainCatcher raincatch;
    public Sprite fullsprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(raincatch.RainTime >= 5){
            spriteRenderer.sprite = fullsprite;
        }
    }
}
