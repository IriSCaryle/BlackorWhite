using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Battely : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speed;
    [SerializeField] float firingInterval;
    [SerializeField] GameObject main_gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] bool isTracking;
    [SerializeField] GameObject bulletSpawnPos;
    [SerializeField] GameObject pivot;
    [SerializeField]float time;
    [SerializeField] TrackingPlayer tracking;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                Fire();
                time = firingInterval;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
            isTracking = true;
            time = firingInterval;
            tracking.Tracking(target);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = null;
            isTracking = false;
            tracking.FinishTracking();
        }
    }

    void Fire()
    {
        Debug.Log("Battely:発射");
        GameObject Bullet = Instantiate(bulletPrefab,bulletSpawnPos.transform.position,Quaternion.identity);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.velocity = pivot.transform.forward * speed;
    }
}
