using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PressBlock : MonoBehaviour
{
    [SerializeField] CinemachineCollisionImpulseSource CinemachineCollisionImpulseSource;

    [SerializeField] Vector2 startPos;
    [SerializeField] float backSpeed;
    [SerializeField] float pressPower;
    [SerializeField] float cooltime;
    [SerializeField] Rigidbody2D rb;
    [SerializeField]bool isStart;
    [SerializeField]bool isCoolDown;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        time = cooltime;
        isCoolDown = false;
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolDown)
        {
            gameObject.transform.localPosition = Vector2.MoveTowards(gameObject.transform.localPosition, startPos, backSpeed * Time.deltaTime);

            if(gameObject.transform.localPosition.y >= startPos.y)
            {
                isCoolDown = false;
                time = cooltime;
                isStart = true;   
            }
        }

        if (isStart)
        {
           
            time -= Time.deltaTime;

            if(time <= 0)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(Vector2.down*pressPower);
                isStart = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Wall"|| collision.gameObject.tag == "Ground")
        {
            CinemachineCollisionImpulseSource.GenerateImpulseAt(gameObject.transform.position, Vector3.down);
            rb.bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine("CollisionCoolDown");
        }
    }


    IEnumerator CollisionCoolDown()
    {
        yield return new WaitForSeconds(1);
        isCoolDown = true;
        yield break;
    }
}
