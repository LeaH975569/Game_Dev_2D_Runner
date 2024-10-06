using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MovePrefab : MonoBehaviour
{
    public GameObject prefabNext;
    public GameObject PrefabCurrent;
    public GameObject FuelCan;
    public GameObject Square1;
    public GameObject Square2;
    public GameObject Laser;
    public List<GameObject> objectList;

    public float incrementxpos; // prefab

    private GameObject PrefabSwap; // where you hold part of the prefab
    private int trigger1; 

    // Start is called before the first frame update
    void Start()
    {
        trigger1 = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision) // what happend when you trigger moving the prefab
    {
        Debug.Log("TRIGGER");
      

        if (trigger1 >= 0 & collision.gameObject.tag == "Prefab Trigger") 
        {

            Debug.Log(trigger1);
            if (trigger1 > 0)  // if trigger > 0
           {
             Debug.Log("End of Prefab Triggered");
             Vector2 tempos = PrefabCurrent.transform.position;
             tempos.x = tempos.x + incrementxpos; // increment the pos that is stored by the amount you want to move it 
             prefabNext.transform.position = tempos; //transform the next prefab to one you want to move into a new pos
             PrefabSwap = PrefabCurrent; // then store the current one somewhere and change the next ones
             PrefabCurrent = prefabNext;
             prefabNext = PrefabSwap;
           }

            collision.gameObject.SetActive(false);
            trigger1++; // add what makes it trigger, not 0, its like a first time switch

            if (trigger1 == 2)
            {
                Square1.SetActive(true);
                Square2.SetActive(true);
                trigger1 = 0;
                Comeback();          
            }

        }

    }

    void Comeback()
    {
        foreach (GameObject objects in objectList)
        {
            objects.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}




