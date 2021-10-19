
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float ShotSpeed;
    public GameObject FiringBlockPrefab;
    public bool freeze;
    [SerializeField]GameObject Bulletpool;
    [SerializeField] BulletPoolManager bulletPoolManager;
    void FixedUpdate()
    {
        if (!freeze)
        {
            if (Input.GetButtonDown("Fire1")) Shot();
        }
    }

    public void Shot()
    {
        GameObject Bullet = bulletPoolManager.FindBullet();
        
  
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