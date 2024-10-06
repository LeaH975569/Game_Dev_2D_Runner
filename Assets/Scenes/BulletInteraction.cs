using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInteraction : MonoBehaviour
{
    public HeroManager hm;
    public GameObject bullet2;
    public float bullet2Speed;
    public Animator gunAnim;
    private Vector3 bullet2Target;
    private Vector3 bullet2Position;
    private bool gun2Triggered;

    // Start is called before the first frame update
    void Start()
    {
        bullet2Position = bullet2.transform.position;
        gun2Triggered = false;
    }

    void FireGun2()
    {
        Debug.Log("Bullet Moving");
        bullet2.transform.position = Vector3.MoveTowards(bullet2.transform.position, bullet2Target, Time.deltaTime * bullet2Speed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletTrigger2")
        {
            Debug.Log("Gun Triggered");
            bullet2.SetActive(true);
            gun2Triggered = true;
            bullet2Target = transform.position;
        }

        if (collision.gameObject.tag == "Bullet2")
        {
            Debug.Log("Bullet hit");
            bullet2.SetActive(false);
            bullet2.transform.position = bullet2Position;
            hm.decreaseLives();
            hm.PlayBulletSound();
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (gun2Triggered == true)
        {
            FireGun2();
        }
    }
}