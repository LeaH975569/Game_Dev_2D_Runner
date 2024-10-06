using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private Vector3 initialpos;
    public BulletInteraction bInt;
    // Start is called before the first frame update
    void Start()
    {
        initialpos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Hero")
        {
            this.gameObject.SetActive(false);
            transform.position = initialpos;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
