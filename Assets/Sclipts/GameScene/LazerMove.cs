using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// レーザー照射動作
/// </summary>
public class LazerMove : MonoBehaviour
{
    [Header("スピード")]
    [SerializeField] float Speed;
    [Header("スプライト")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [Header("レーザーの距離")]
    [SerializeField] float Limit;
    [Header("照射判定")]
     public bool isFire;
     public bool isUnFire;
    // Start is called before the first frame update
    void Start()
    {
        isFire = false;
        isUnFire = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFire)//照射動作
        {
            Vector2 vec = new Vector2(0, Speed * Time.deltaTime);
            spriteRenderer.size += vec;
            Debug.Log("isFire:"+ vec);
            if(spriteRenderer.size.y >= Limit)
            {
                isFire = false;
            }
        }
        if (isUnFire)//解除動作
        {
            Vector2 vec = new Vector2(0, Speed * Time.deltaTime);
            spriteRenderer.size -= vec;
            Debug.Log("isUnFire:" + vec);
            if (spriteRenderer.size.y <= 0)
            {
                isUnFire = false;
            }
        }
    }

    public void Fire()
    {
        isFire = true;
    }
    public void UnFire()
    {
        isUnFire = true;
    }
}
