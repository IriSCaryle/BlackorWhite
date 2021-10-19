using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMove : MonoBehaviour
{

    [SerializeField] float Speed;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float Limit;
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
        if (isFire)
        {
            Vector2 vec = new Vector2(0, Speed * Time.deltaTime);
            spriteRenderer.size += vec;
            Debug.Log("isFire:"+ vec);
            if(spriteRenderer.size.y >= Limit)
            {
                isFire = false;
            }
        }
        if (isUnFire)
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
