using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Lea's 
public class Bullet : MonoBehaviour
{
    private Vector2 originalPosition;
    private bool isReturning = false;
    public float bulletmove; // Add this field to define bullet speed

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        // Check if the bullet should return to its original position
        if (isReturning)
        {
            // Move the bullet back to the original position
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, bulletmove * Time.deltaTime);

            // If the bullet has reached the original position, deactivate it
            if (Vector2.Distance(transform.position, originalPosition) < 0.01f)
            {
                gameObject.SetActive(false); // Deactivate the bullet
                isReturning = false; // Reset the returning flag
            }
        }
    }
 
}
