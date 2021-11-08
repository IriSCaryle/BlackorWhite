using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBall : MonoBehaviour
{

    [SerializeField] Collider2D startWall;

    [SerializeField] GameObject endpos;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float ballSpeed;

    [SerializeField]bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStart)
        {
            rb.gameObject.transform.position = Vector2.MoveTowards(rb.gameObject.transform.position,endpos.transform.position,ballSpeed*Time.deltaTime);
        }

        if(rb.gameObject.transform.position.x >= endpos.transform.position.x)
        {
            OnStop();
        }
    }

    public void OnStart()
    {
        startWall.enabled = false;
        isStart = true;
    }

    public void OnStop()
    {
        isStart = false;
        startWall.enabled = true;
        rb.gameObject.SetActive(false);
    }
}
