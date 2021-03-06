
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
    [Header("Audio")]
    [SerializeField] AudioSource aud_SE;
    [SerializeField] AudioClip SE_blockFire;
    int FireNum;
    void Update()
    {
        if (!PleyerSclipt.freeze)
        {
            if (Input.GetButtonDown("Fire1")) Shot();
        }
    }

    public (bool,int) AchievementClear()
    {
        if(FireNum == 0)
        {
            return (true,5);
        }
        else
        {
            return (false, 0);
        }
    }

    public void Shot()//ブロック動作
    {
        GameObject Bullet = bulletPoolManager.FindBullet();//プールマネージャから発射可能なオブジェクトを検索・取得

        Invert invert = Bullet.GetComponent<Invert>();
        invert.BulletCheckColor();
        //発射動作
        if (this.transform.parent.localScale.x >= 1)
        {
            aud_SE.clip = SE_blockFire;
            aud_SE.Play();
            Rigidbody2D BulletRigidBody2D = Bullet.GetComponent<Rigidbody2D>();
            Wallstick wallstick = Bullet.GetComponent<Wallstick>();
            wallstick.rb.bodyType = RigidbodyType2D.Dynamic;
            Bullet.transform.position = gameObject.transform.position;
            
            BulletRigidBody2D.AddForce(Vector2.right * ShotSpeed);
            Debug.Log("Bullet:Addforce");
            FireNum += 1;
        }
        else if(this.transform.parent.localScale .x < 0)
        {
            aud_SE.clip = SE_blockFire;
            aud_SE.Play();
            Rigidbody2D BulletRigidBody2D = Bullet.GetComponent<Rigidbody2D>();
            Wallstick wallstick = Bullet.GetComponent<Wallstick>();
            wallstick.rb.bodyType = RigidbodyType2D.Dynamic;
            Bullet.transform.position = gameObject.transform.position;
            
            BulletRigidBody2D.AddForce(Vector2.left * ShotSpeed);
            Debug.Log("Bullet:Addforce");
            FireNum += 1;
        }
    }
}