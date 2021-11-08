using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movefloor : MonoBehaviour
{
    //変数定義
    Rigidbody2D rb;
    SurfaceEffector2D surfaceEffector;
    public int speed;
    public int movepos;
    Vector2 DefaultPos;
    Vector2 PrevPos;
    [SerializeField]vector vector_Type;
    

    enum vector
    {
        vertical,
        horizontal,
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DefaultPos = transform.position;
        surfaceEffector = GetComponent<SurfaceEffector2D>();

       
    }

    void FixedUpdate()
    {
        
            if (vector_Type == vector.horizontal)
            {
                PrevPos = rb.position;

                // X座標のみ横移動　Mathf.PingPongの数値部分変更で移動距離が変わる
                Vector2 pos = new Vector2(DefaultPos.x + Mathf.PingPong(Time.time * speed, movepos), DefaultPos.y);
                rb.MovePosition(pos);

                // 速度を逆算する
                Vector2 velocity = (pos - PrevPos) / Time.deltaTime;

                // 速度のX成分を SurfaceEffector2D に適用
                surfaceEffector.speed = velocity.x;
            }

            if (vector_Type == vector.vertical)
            {
                PrevPos = rb.position;

                // X座標のみ横移動　Mathf.PingPongの数値部分変更で移動距離が変わる
                Vector2 pos = new Vector2(DefaultPos.x, DefaultPos.y + Mathf.PingPong(Time.time * speed, movepos));
                rb.MovePosition(pos);

                // 速度を逆算する
                Vector2 velocity = (pos - PrevPos) / Time.deltaTime;

                // 速度のX成分を SurfaceEffector2D に適用
                surfaceEffector.speed = velocity.y;
            }

        
    }

   
}