using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スイッチ動作
/// </summary>
public class Switch : MonoBehaviour
{
    [Header("開位置")]
    public Vector2 leverOnPos;
    [Header("閉位置")]
    public Vector2 leverOffPos;
    [Header("Onであるか")]
    public bool isOn;
    [Header("レバーオブジェクト")]
    public GameObject lever;
    [Header("レバー速度")]
    public float speed;

    bool isRange;
    
    [SerializeField] PopUpUI popUpUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnOffSwitch();
        CheckOnTrigger();
    }

    void OnOffSwitch()//レバー動作
    {
        if (isOn)
        {
            lever.transform.localPosition = Vector2.MoveTowards(lever.transform.localPosition, leverOnPos, speed * Time.deltaTime);
        }
        if (!isOn)
        {
            lever.transform.localPosition = Vector2.MoveTowards(lever.transform.localPosition, leverOffPos, speed * Time.deltaTime);
        }
    }

    void CheckOnTrigger()//キー判定
    {
        if (isRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOn = !isOn;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isRange = true;
            popUpUI.isFadeIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isRange = false;
            popUpUI.isFadeOut = true;
        }
    }

}
