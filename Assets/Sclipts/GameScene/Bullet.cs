using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 放つブロックのスクリプト
/// </summary>
public class Bullet : MonoBehaviour
{
    public bool isActive;//ブロックを使用しているかどうか(プールオブジェクトのため)
    public float life;//壁にぶつかってから生存する時間
    [SerializeField]float nowLife;//残り生存時間
    bool iscollision;//壁や弾にぶつかったか
    public Vector2 defaultPos;//プール位置
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
