using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤースクリプト
/// </summary>
public class PleyerSclipt : MonoBehaviour
{   
    [Header("速度")]
    [SerializeField] float speed;
    [Header("ジャンプ力")]
    [SerializeField] float flap;
    [Header("制限速度")]
    [SerializeField] float limitspeed;
    [Header("力の向き")]
    [SerializeField] Vector2 force;
    [Header("各アニメーター")]
    [SerializeField] Animator animator;
    [SerializeField] Animator DeadAnimator;
    [Header("ブロックが発射される親")]
    [SerializeField] GameObject Shooter;
    [Header("死亡パーティクル")]
    [SerializeField] ParticleSystem DeadParticle;
    [Header("リグ親")]
    [SerializeField] GameObject Rigs;
    [Header("カプセルコライダー")]
    [SerializeField] CapsuleCollider2D playerCollider;
    Quaternion ShooterRot;
    private Rigidbody2D rb;
    bool isJump = false;
    [Header("プレイヤー停止")]
    public bool freeze;
    [Header("アビリティ使用")]
    public bool avility;
    [Header("色反転のクールタイム")]
    public float limitCoolTime =3;
    [Header("現在のクールタイム")]
    public float nowCoolTime;

    bool avilityCoolTime;
    [Header("現在の世界の色")]
    public WorldType worldType;
    [Header("初期の世界の色")]
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
            //---ジャンプ動作---//
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
        //---------移動動作----------//
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
    void AvilityCoolCountDown()//色反転アビリティのクールダウン動作
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

    void ObserbKeys()//アビリティ発動キーの監視
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
    void OnCollisionEnter2D(Collision2D other)//各コライダーとぶつかった時の動作
    {
        
        animator.SetBool("ground", true);
        animator.ResetTrigger("jump");
        isJump = false;

        if (other.gameObject.tag == "Enemy")
        {
            PlayerDead();
        }
        if(other.gameObject.tag == "DeadZone")
        {
            PlayerLazerDead();
        }

        if(other.gameObject.tag == "Goal")
        {
            PlayerGoal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            PlayerLazerDead();
        }
    }


    public void PlayerGoal()//ゴール時の動作
    {
        freeze = true;
        animator.SetInteger("speed", 0);
    }

    public void PlayerDead()//死んだ時の動作と演出再生
    {
        freeze = true;
        animator.SetInteger("speed", 0); 
        StartCoroutine("Dead");
    }

    public void PlayerLazerDead()//レーザーで死亡した際の動作と演出再生
    {
        freeze = true;
        animator.SetInteger("speed", 0);
        Rigs.SetActive(false);
        DeadParticle.Play();

        
        StartCoroutine("LazerDead");
    }

    IEnumerator LazerDead()
    {
        yield return new WaitForSeconds(0.2f);
        rb.bodyType = RigidbodyType2D.Kinematic;
        playerCollider.enabled = false;
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

    void OnParticleSystemStopped()//パーティクルが終わった際に時間速度を０にする
    {
        Time.timeScale = 0;
    }



   
}