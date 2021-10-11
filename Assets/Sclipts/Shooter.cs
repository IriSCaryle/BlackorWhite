
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float ShotSpeed;
    public GameObject FiringBlockPrefab;
    public bool freeze;
    void FixedUpdate()
    {
        if (!freeze)
        {
            if (Input.GetButtonDown("Fire1")) Shot();
        }
    }

    public void Shot()
    {
        GameObject Bullet = (GameObject)Instantiate(FiringBlockPrefab, transform.position, Quaternion.identity);
        var parentTransform = this.transform.parent.localScale;
        if (this.transform.parent.localScale.x >= 1)
        {
            Rigidbody2D BulletRigidBody2D = Bullet.GetComponent<Rigidbody2D>();
            BulletRigidBody2D.AddForce(Vector2.right * ShotSpeed);
        }
        else if(this.transform.parent.localScale .x < 0)
        {
            Rigidbody2D BulletRigidBody2D = Bullet.GetComponent<Rigidbody2D>();
            BulletRigidBody2D.AddForce(Vector2.left * ShotSpeed);
        }
    }
}