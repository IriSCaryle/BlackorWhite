
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float ShotSpeed;
    public GameObject FiringBlockPrefab;
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    public void Shot()
    {
        GameObject Bullet = (GameObject)Instantiate(FiringBlockPrefab, transform.position, Quaternion.identity);
        Rigidbody BulletRigidBody = Bullet.GetComponent<Rigidbody>();
        BulletRigidBody.AddForce(Vector2.one * ShotSpeed);
    }
}