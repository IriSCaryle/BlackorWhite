using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// レーザー
/// </summary>
public class Lazer : MonoBehaviour
{
    [Header("動作タイプ")]
    [SerializeField] MoveType moveType;

    [Header("Common")]
    [SerializeField] bool unlimited;
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
    [SerializeField] LazerMove lazerMove;
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
    [Header("ゲームマネージャー")]
    [SerializeField] GameManager gameManager;
    [Header("プレイヤー")]
    [SerializeField] PleyerSclipt pleyerSclipt;
    [Header("デフォルトの色")]
    [SerializeField]DefaultColor defaultColor;

   
    enum MoveType
    {
        NoMove = 1,
        Rotate = 2,
    }

    enum DefaultColor
    {
        Black =0,
        White =1,
    }


    void Awake()
    {
        switch ((int)moveType)
        {
            case 1:
                if (unlimited)
                {
                    if (lazerMove != null)
                    {
                        lazerMove.Fire();
                    }
                }
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
    }
    void Start()
    {
        Debug.Log("Lazer:moveType:" + (int)moveType);
    }
    void Update()
    {
        switch ((int)moveType)
        {
            case 1:
                if (!unlimited)
                {
                    Count();
                }
                else
                {
                    UnlimitedRay();
                }
                break;
            case 2:
               
                MoveRay();
                break;
        }

       
        CoolCountDown();
    }


    void UnlimitedRay()
    {
        RaycastHit2D hit2;
        Debug.Log("Lazer:レーザー展開中");
        hit2 = Physics2D.Raycast(lazerPivot.transform.position, lazerPivot.transform.forward, distance);
        Debug.DrawRay(lazerPivot.transform.position, lazerPivot.transform.forward * hit2.distance, Color.red, hit2.distance, false);
        if (hit2.collider)
        {
            if (hit2.collider.gameObject.tag == "Wall"|| hit2.collider.gameObject.tag == "Ground")
            {

                Debug.Log("Lazer:通過可能");
                if (lazerMove.CanChangeRange(hit2.distance))
                {
                    lazerMove.ChangeRangeFire(hit2.distance);
                }
            }
            else if (hit2.collider.gameObject.tag == "Player")
            {
                Debug.Log("Lazer:あたった");
                gameManager.PlayerLazerDead();
            }
        }
       
    }

    void MoveRay()//レーザーの回転に沿ってRayを発射する動作
    {
        if ((int)defaultColor != (int)pleyerSclipt.worldType)
        {
            hit = Physics2D.Raycast(lazerPivot.transform.position, lazerPivot.transform.forward, distance);
            Debug.DrawRay(lazerPivot.transform.position, lazerPivot.transform.forward * hit.distance, Color.red, 10, false);
            if (hit.collider)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Lazer:あたった");
                    gameManager.PlayerLazerDead();
                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch ((int)moveType)
        {
            case 1:
                break;
            case 2:
                if (isStart)//レーザーを左に回転させる動作
                {
                    if (lazerMove != null)
                    {
                        lazerMove.Fire();
                    }
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
                if (isFinish)//レーザーを右に回転させる動作
                {
                    if (lazerMove != null)
                    {
                        lazerMove.UnFire();
                    }
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

 
    void CoolCountDown()//クールタイムを数える動作
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
                if (lazerMove != null)
                {
                    lazerMove.Fire();
                }

            }
        }
    }

    void Count()//レーザーが照射されている時間を計り、オンオフする動作
    {
        if (isOn)
        {
            nowCountTime -= Time.deltaTime;

            if ((int)defaultColor != (int)pleyerSclipt.worldType)
            {
                Debug.Log("Lazer:レーザー展開中");
                hit = Physics2D.Raycast(lazerPivot.transform.position, lazerPivot.transform.forward, distance);
                Debug.DrawRay(lazerPivot.transform.position, lazerPivot.transform.forward * hit.distance, Color.red, 10, false);
                if (hit.collider)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("Lazer:あたった");
                        gameManager.PlayerLazerDead();
                    }
                }

            }
            if (nowCountTime < 0)
            {
                if (lazerMove != null)
                {
                    lazerMove.UnFire();
                }
                Debug.Log("Lazer:レーザー終了");
                isOn = false;
                nowCoolTime = coolTime;
                coolDown = true;
            }
        }
    }
  

}
