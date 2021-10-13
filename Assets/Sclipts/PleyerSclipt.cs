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
    [SerializeField] GameObject Shooter;
    Quaternion ShooterRot;
    private Rigidbody2D rb;
    bool isJump = false;
    public bool freeze;
    public bool avility;
    public WorldType worldType;

    public WorldType defaultWorld;
    public enum WorldType 
    {
        Black =0,
        White =1,
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        worldType = WorldType.Black;
    }
    private void Update()
    {
        ObserbKeys();
    }
    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        
        if (!freeze)
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
                    ShooterRot = Shooter.transform.rotation;
                    ShooterRot.y = 180;
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
                rb.AddForce(Vector2.up * flap, ForceMode2D.Force);
                isJump = true;
            }
        }

    }


    void ObserbKeys()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            avility = true;
            switch (worldType)
            {
                case WorldType.Black:
                    worldType = WorldType.White;
                    break;
                case WorldType.White:
                    worldType = WorldType.Black;
                    break;
            }
        }
        else
        {
            avility = false;
        }
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