using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ブロックのくっつく動作
/// </summary>
public class Wallstick : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision .gameObject.tag=="Wall"|| collision.gameObject.tag=="Bullet" )
            {
                
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
    }
}
