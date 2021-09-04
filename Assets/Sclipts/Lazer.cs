using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    [SerializeField] MoveType moveType;

    [Header("Common")]
    [SerializeField] float time;
    [SerializeField] float coolTime;
    [SerializeField] float distance;
    [SerializeField] GameObject lazerParent;
    [SerializeField] GameObject lazerPivot;
    [SerializeField] GameObject lazer;

    bool isStart = false;
    bool isFinish = false;
    bool isWrap = false;
    bool coolDown = false;
    float nowCoolTime;
    [Header("NoMove")]
    [SerializeField] Vector3 axis;
    [SerializeField] float angle;
    [Header("Rotate")]
    [SerializeField] float moveTime;
    [SerializeField] float nowtimeS;
    [SerializeField] float nowtimeF;
    [SerializeField] float angle1;
    [SerializeField] float angle2;
    [Header("MoveHorizontal")]
    [SerializeField] Vector3 pos1;
    [SerializeField] Vector3 pos2;
    RaycastHit2D hit;

    enum MoveType
    {
        NoMove = 1,
        Rotate = 2,
        MoveHorizontal = 3
    }


    void Awake()
    {
        switch ((int)moveType)
        {
            case 1:
                lazer.transform.rotation = Quaternion.AngleAxis(angle, axis);
                isStart = true;
                isFinish = false;
                coolDown = false;
                isWrap = false;
                break;
            case 2:
                isStart = true;
                isFinish = false;
                coolDown = false;
                isWrap = false;
                break;
            case 3:
                isStart = true;
                isFinish = false;
                coolDown = false;
                isWrap = false;
                break;
        }

        Application.targetFrameRate = 60;

        nowCoolTime = coolTime;
    }
    void Start()
    {

    }
    void Update()
    {
        
        hit = Physics2D.Raycast(lazerPivot.transform.position, lazerPivot.transform.forward, distance);
        Debug.DrawRay(lazerPivot.transform.position, lazerPivot.transform.forward * hit.distance, Color.red,10,false);
        if (hit.collider)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("あたった");
            }
        }
        CountDown();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch ((int)moveType)
        {
            case 1:


                break;
            case 2:
                if (isStart)
                {
                    nowtimeS += moveTime * Time.deltaTime;
                    float _angleS = Mathf.LerpAngle(angle1, angle2, nowtimeS);
                    lazerParent.transform.eulerAngles = new Vector3(0, 0, _angleS);
                    Debug.Log("_angleS:"+_angleS);
                    if (_angleS == angle2)
                    {
                        nowCoolTime = coolTime;
                        isStart = false;
                        isWrap = true;
                        coolDown = true;
                    }
                }
                if (isFinish)
                {
                    nowtimeF += moveTime * Time.deltaTime;
                    float _angleF = Mathf.LerpAngle(angle2, angle1, nowtimeF);
                    lazerParent.transform.eulerAngles = new Vector3(0, 0, _angleF);
                    Debug.Log("_angleF:" + _angleF);
                    if (_angleF == angle1)
                    {
                        nowCoolTime = coolTime;
                        isFinish = false;
                        isWrap = false;
                        coolDown = true;
                    }
                }    
                break;
            case 3:
               
                break;

        }



    }

 
    void CountDown()
    {
        if (coolDown)
        {

            nowCoolTime -= Time.deltaTime;

            if (nowCoolTime < 0)
            {
                if (isWrap)
                {
                    nowtimeF = 0;
                    isFinish = true;
                }
                else
                {
                    nowtimeS = 0;
                    isStart = true;
                }
                coolDown = false;
            }
        }
    }

  
  

}
