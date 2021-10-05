using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallstick : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (GameObject.FindGameObjectWithTag("Wall"))
            {
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
    }
}
