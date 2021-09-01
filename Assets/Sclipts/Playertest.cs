using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playertest : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _flap;
    private Rigidbody2D rb;
    bool isJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        float horizontalKey = Input.GetAxisRaw("Horizontal");

        //右入力で左向きに動く
        if (horizontalKey > 0)
        {
            rb.velocity = new Vector2(_speed, rb.velocity.y);
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            rb.velocity = new Vector2(-_speed, rb.velocity.y);
        }
        //ボタンを話すと止まる



        if (Input.GetKeyDown("space") && !isJump)
        {
            rb.AddForce(Vector2.up * _flap);
            isJump = true;
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        isJump = false;
    }
}