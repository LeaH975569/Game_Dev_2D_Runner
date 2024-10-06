using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollower : MonoBehaviour
{
    public GameObject HeroMirror;
    private SpriteRenderer spriteRenderer;
    public float HeroYpos;
    public float CameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = HeroMirror.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.flipX == false)
        {
            // Hero moving to the right
            if ((this.transform.position.x - HeroMirror.transform.position.x) <= CameraOffset)
            {
                this.transform.position = new Vector2(HeroMirror.transform.position.x + CameraOffset, HeroYpos);
            }
        }

    }
}
