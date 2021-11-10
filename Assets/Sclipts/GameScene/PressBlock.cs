using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PressBlock : MonoBehaviour
{
    public CinemachineCollisionImpulseSource CinemachineCollisionImpulseSource;
    public Vector2 startPos;
    public int startRandomCoolTimeRangeMin;
    public int startRandomCoolTimeRangeMax;
    public int pressTime;
    public float backSpeed;
    public float pressPower;
    public float cooltime;
    public Rigidbody2D rb;
    public bool isStart;
    public bool isCoolDown;
    public PressTimeType pressTimeType;
    float time;


    public enum PressTimeType
    {
        Random,
        cons,
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (pressTimeType)
        {
            case PressTimeType.cons:
                time = pressTime;
                break;
            case PressTimeType.Random:
                int random = Random.Range(startRandomCoolTimeRangeMin, startRandomCoolTimeRangeMax);
                Debug.Log("Press_RandomTime:" + gameObject.name + "=" + random);
                time = random;
                break;
        }
       
        rb.bodyType = RigidbodyType2D.Kinematic;
       
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
            CinemachineCollisionImpulseSource.GenerateImpulseAt(gameObject.transform.position, Vector2.down);
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
