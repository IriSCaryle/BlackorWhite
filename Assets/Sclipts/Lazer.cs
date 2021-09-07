using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    [SerializeField] MoveType moveType;

    [Header("Common")] 
    [SerializeField] float coolTime;
    [SerializeField] float distance;
    [SerializeField] GameObject lazerParent;
    [SerializeField] GameObject lazerPivot;
    [SerializeField] GameObject lazer;
    bool coolDown = false;
    [SerializeField] float nowCoolTime;
    [Header("NoMove")]
    [SerializeField] float time;
    [SerializeField] Vector3 axis;
    [SerializeField] float angle;
    bool isOn = false;
    [SerializeField]float nowCountTime;
    [Header("Rotate")]
    [SerializeField] float moveTime;
    [SerializeField] float nowtimeS;
    [SerializeField] float nowtimeF;
    [SerializeField] float angle1;
    [SerializeField] float angle2;
    bool isStart = false;
    bool isFinish = false;
    bool isWrap = false;
    RaycastHit2D hit;

    enum MoveType
    {
        NoMove = 1,
        Rotate = 2,
    }


    void Awake()
    {
        switch ((int)moveType)
        {
            case 1:
                lazerParent.transform.rotation = Quaternion.AngleAxis(angle, axis);
                coolDown = false;
  
                isOn = true;
                nowCountTime = time;
                nowCoolTime = coolTime;
                break;
            case 2:
                isStart = true;
                isFinish = false;
                coolDown = false;
                isWrap = false;
                nowCoolTime = coolTime;
                break;
        }

        Application.targetFrameRate = 60;

        
    }
    void Start()
    {

    }
    void Update()
    {
        switch ((int)moveType)
        {
            case 1:
                Count();              
                break;
            case 2:
                hit = Physics2D.Raycast(lazerPivot.transform.position, lazerPivot.transform.forward, distance);
                Debug.DrawRay(lazerPivot.transform.position, lazerPivot.transform.forward * hit.distance, Color.red, 10, false);
                if (hit.collider)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("あたった");
                    }
                }
                break;
            case 3:
                hit = Physics2D.Raycast(lazerPivot.transform.position, lazerPivot.transform.forward, distance);
                Debug.DrawRay(lazerPivot.transform.position, lazerPivot.transform.forward * hit.distance, Color.red, 10, false);
                if (hit.collider)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("あたった");
                    }
                }
                break;
        }

       
        CoolCountDown();
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
                 //   Debug.Log("_angleS:"+_angleS);
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
                  //  Debug.Log("_angleF:" + _angleF);
                    if (_angleF == angle1)
                    {
                        nowCoolTime = coolTime;
                        isFinish = false;
                        isWrap = false;
                        coolDown = true;
                    }
                }    
                break;
        }
    }

 
    void CoolCountDown()
    {
        if (coolDown)
        {

            nowCoolTime -= Time.deltaTime;

            if (nowCoolTime < 0)
            {
                switch ((int)moveType)
                {
                    case 1:
                        nowCountTime = time;
                        isOn = true;
                        break;
                    case 2:
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
                        
                        break;
                  
                    
                }
                coolDown = false;

            }
        }
    }

    void Count()
    {
        if (isOn)
        {
            nowCountTime -= Time.deltaTime;

            
            Debug.Log("レーザー展開中");
            hit = Physics2D.Raycast(lazerPivot.transform.position, lazerPivot.transform.forward, distance);
            Debug.DrawRay(lazerPivot.transform.position, lazerPivot.transform.forward * hit.distance, Color.red, 10, false);
            if (hit.collider)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("あたった");
                }
            }

            if (nowCountTime < 0)
            {
                Debug.Log("レーザー終了");
                isOn = false;
                nowCoolTime = coolTime;
                coolDown = true;
            }
        }
    }
  

}
