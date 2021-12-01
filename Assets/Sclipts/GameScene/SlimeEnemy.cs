using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particle;
    [SerializeField] Collider2D collider2D;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] EnemyType enemyType;
    [SerializeField] EnemyTargetPos enemyTargetPos;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    public GameObject player;

    [SerializeField] GameObject pos1;
    [SerializeField] GameObject pos2;
    [SerializeField] AudioSource aud_SE;
    public bool chase;
    bool moveReady;

    enum EnemyType
    {
        None,
        Chase,
        Move,
    }

    enum EnemyTargetPos
    {
        Pos1 = 1,
        Pos2 = 2,
    }
    void Start()
    {
        switch (enemyType)
        {
            case EnemyType.None:
                moveReady = false;
                break;
            case EnemyType.Chase:
                moveReady = true;
                break;
            case EnemyType.Move:
                enemyTargetPos = EnemyTargetPos.Pos1;
            
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    { 
        if (moveReady)
        {
            if (chase)
            {
                animator.SetBool("walk", true);
                Vector2 vec = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime) ;
                vec.y = transform.position.y;
                rb.MovePosition(vec);
            }
        }

        if(enemyType == EnemyType.Move && (int)enemyTargetPos == 1)
        {
            Vector2 vec = Vector2.MoveTowards(transform.position, pos1.transform.position, speed * Time.deltaTime);
            rb.MovePosition(vec);
            float distance = transform.position.x - pos1.transform.position.x;
            //Debug.Log("distance:"+distance);
            if (distance<= 0.01)
            {
                enemyTargetPos = EnemyTargetPos.Pos2;
            }
          
        }
        else if(enemyType == EnemyType.Move && (int)enemyTargetPos == 2)
        {
            Vector2 vec = Vector2.MoveTowards(transform.position, pos2.transform.position, speed * Time.deltaTime);
            rb.MovePosition(vec);
            float distance =  pos2.transform.position.x-transform.position.x ;
            //Debug.Log("distance:" + distance);
            if (distance <= 0.01)
            {
                enemyTargetPos = EnemyTargetPos.Pos1;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            aud_SE.Play();
            CheckColor();
            particle.Play();
            collider2D.enabled = false;
            Color color = sprite.color;
            color.a = 0;
            sprite.color = color;
        }
        else if(collision.gameObject.tag == "Player")
        {
            chase = false;
            animator.SetBool("walk",false);
        }
    }



    void CheckColor()
    {
        if (sprite.color == Color.black)//sprite.color == Color.white)
        {
            Debug.Log("invert:白");
            //sprite.color = Color.white;
            particle.startColor = Color.black;
        }
        else if (sprite.color == Color.white) //sprite.color == Color.black)
        {
            Debug.Log("invert:黒");
            particle.startColor = Color.white;
        }
    }
    
}
