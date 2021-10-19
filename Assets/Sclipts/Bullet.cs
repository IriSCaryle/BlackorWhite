using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isActive;
    public float life;
    [SerializeField]float nowLife;
    bool iscollision;
    public Vector2 defaultPos;
    // Start is called before the first frame update
    
    void Start()
    {
        nowLife = life;
    }

    // Update is called once per frame
    void Update()
    {

        if (iscollision)
        {
            nowLife -= Time.deltaTime;

            if(nowLife <= 0)
            {
                iscollision = false;
                nowLife = life;
                isActive = false;
                this.gameObject.transform.position = defaultPos;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Wall"|| collision.gameObject.tag == "Bullet")
        {
            iscollision = true;
        }
    }
}
