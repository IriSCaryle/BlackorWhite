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
    [SerializeField] Animator DeadAnimator;
    [SerializeField] GameObject Shooter;
    [SerializeField] ParticleSystem DeadParticle;
    [SerializeField] GameObject Rigs;
    [SerializeField] CapsuleCollider2D playerCollider;
    Quaternion ShooterRot;
    private Rigidbody2D rb;
    bool isJump = false;
    public bool freeze;
    public bool avility;
    [Header("色反転のクールタイム")]
    public float limitCoolTime =3;
    public float nowCoolTime;
    bool avilityCoolTime;
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
        
        if (!freeze)
        {
            ObserbKeys();
            AvilityCoolCountDown();

            if (Input.GetKeyDown("space") && !isJump)
            {
                animator.SetTrigger("jump");
                animator.SetBool("ground", false);
                rb.AddForce(Vector2.up * flap, ForceMode2D.Force);
                isJump = true;
            }
        }
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
        }


    }
    void AvilityCoolCountDown()
    {
        if (avilityCoolTime)
        {
            nowCoolTime += Time.deltaTime;

            if (nowCoolTime >= limitCoolTime)
            {
                nowCoolTime = 0;
                avilityCoolTime = false;
            }
        }
    }

    void ObserbKeys()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !avilityCoolTime)
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
            avilityCoolTime = true;
        }
        else
        {
           avility = false;
            
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        animator.SetBool("ground", true);
        isJump = false;

        if (other.gameObject.tag == "Enemy")
        {
            PlayerDead();
        }

        if(other.gameObject.tag == "Enemy")
        {
            PlayerGoal();
        }
    }

    

    public void PlayerGoal()
    {
        freeze = true;
        animator.SetInteger("speed", 0);
    }

    public void PlayerDead()
    {
        freeze = true;
        animator.SetInteger("speed", 0); 
        StartCoroutine("Dead");
    }

    public void PlayerLazerDead()
    {
        freeze = true;
        animator.SetInteger("speed", 0);
        Rigs.SetActive(false);
        DeadParticle.Play();
       
        StartCoroutine("LazerDead");
    }

    IEnumerator LazerDead()
    {
        yield return new WaitForSeconds(2);
        DeadAnimator.SetTrigger("GameOver");
        Debug.Log("Dead:終了");
        Debug.Log("timescale1");
        yield break;
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1);
        Rigs.SetActive(false);
        rb.bodyType = RigidbodyType2D.Kinematic;
        playerCollider.enabled = false;
        DeadParticle.Play();        
        yield return new WaitForSeconds(2);
        DeadAnimator.SetTrigger("GameOver");
        Debug.Log("Dead:終了");
        yield break;
    }

    void OnParticleSystemStopped()
    {
        Time.timeScale = 0;
    }



    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}