using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleyerSclipt : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float flap;
    [SerializeField] float limitspeed;
    [SerializeField] Vector2 force;
    [SerializeField] Animator animator;
    private Rigidbody2D rb;
    bool isJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        float _horizontalKey = Input.GetAxisRaw("Horizontal");
        animator.SetInteger("speed", (int)_horizontalKey);
        //右入力で左向きに動く
        if (_horizontalKey > 0)
        {
            if (this.gameObject.transform.localScale.x < 0)
            {
                this.transform.localScale = new Vector2(1, 1);
            }

            rb.velocity = new Vector2(speed, rb.velocity.y);

            

            if (rb.velocity.magnitude > limitspeed)
            {
                rb.AddForce(-force);
            }
        }
        //左入力で左向きに動く
        else if (_horizontalKey < 0)
        {
            if (this.gameObject.transform.localScale.x > 0)
            {
                this.transform.localScale = new Vector2(-1, 1);
            }

            rb.velocity = new Vector2(-speed, rb.velocity.y);

            

            if (rb.velocity.magnitude > limitspeed)
            {
                rb.AddForce(force);
            }
        }

        if (Input.GetKeyDown("space") && !isJump)
        {
             animator.SetTrigger("jump");
             animator.SetBool("ground", false);
             rb.AddForce(Vector2.up * flap ,ForceMode2D.Force);
             isJump = true;
        }

        Debug.Log(_horizontalKey);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        animator.SetBool("ground",true);
        isJump = false;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}