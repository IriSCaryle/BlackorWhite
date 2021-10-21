
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ブロックを発射するスクリプトなど プールスクリプトとの橋渡し
/// </summary>
public class Shooter : MonoBehaviour
{
    [Header("発射速度")]
    [SerializeField] float ShotSpeed;
    [Header("ブロックのプレハブ")]
    public GameObject FiringBlockPrefab;
    [Header("プレイヤースクリプト")]
    [SerializeField] PleyerSclipt PleyerSclipt;
    [Header("プールオブジェクト")]
    [SerializeField]GameObject Bulletpool;
    [Header("プールマネージャー")]
    [SerializeField] BulletPoolManager bulletPoolManager;
    void Update()
    {
        if (!PleyerSclipt.freeze)
        {
            if (Input.GetButtonDown("Fire1")) Shot();
        }
    }

    public void Shot()//ブロック動作
    {
        GameObject Bullet = bulletPoolManager.FindBullet();//プールマネージャから発射可能なオブジェクトを検索・取得
        
        //発射動作
        if (this.transform.parent.localScale.x >= 1)
        {
            Bullet.transform.position = gameObject.transform.position;
            Rigidbody2D BulletRigidBody2D = Bullet.GetComponent<Rigidbody2D>();
            Wallstick wallstick = Bullet.GetComponent<Wallstick>();
            wallstick.rb.bodyType = RigidbodyType2D.Dynamic;
            BulletRigidBody2D.AddForce(Vector2.right * ShotSpeed);
            Debug.Log("Bullet:Addforce");
        }
        else if(this.transform.parent.localScale .x < 0)
        {
            Bullet.transform.position = gameObject.transform.position;
            Rigidbody2D BulletRigidBody2D = Bullet.GetComponent<Rigidbody2D>();
            Wallstick wallstick = Bullet.GetComponent<Wallstick>();
            wallstick.rb.bodyType = RigidbodyType2D.Dynamic;
            BulletRigidBody2D.AddForce(Vector2.left * ShotSpeed);
            Debug.Log("Bullet:Addforce");
        }
    }
}