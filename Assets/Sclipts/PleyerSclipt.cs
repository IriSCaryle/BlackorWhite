using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleyerSclipt : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _flap;
    [SerializeField] float _limitspeed;
    [SerializeField] Vector2 force;
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
            rb.AddForce(Vector2.right * _speed);
            if (rb.velocity.magnitude > _limitspeed)
            {
                rb.AddForce(-force);
            }
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            rb.AddForce(Vector2.left * _speed);
            if (rb.velocity.magnitude > _limitspeed)
            {
                rb.AddForce(force);
            }
        }
        //ボタンを話すと止まる
        else
        {
            rb.velocity = Vector2.zero;
        }


        if (Input.GetKeyDown("space") && !isJump)
            {
                rb.AddForce(Vector2.up * _flap);
                isJump = true;
            }
        Application.targetFrameRate = 60;

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        isJump = false;
    }
}