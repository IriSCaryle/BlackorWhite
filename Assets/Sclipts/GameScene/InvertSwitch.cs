using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スイッチのオンオフが反対のスイッチ動作
/// 基本的にはSwitchと同じ
/// </summary>
public class InvertSwitch : MonoBehaviour
{
  public Vector2 leverOnPos;//On状態の位置

    public Vector2 leverOffPos;//off状態の位置

    public bool isOn;//Onであるか

    public GameObject lever;//レバー部分

    public float speed;//移動速度

    bool isRange;//範囲内にいるか
    [SerializeField] PopUpUI popUpUI;//ガイド表示

    [SerializeField] AudioSource SE_audSource;

    [SerializeField] PleyerSclipt player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PleyerSclipt>() ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnOffSwitch();
        CheckOnTrigger();
    }

    void OnOffSwitch()//スイッチ移動動作
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

    void CheckOnTrigger()//範囲内に入っている時の
    {
        if (isRange)
        {
            if (!player.freeze)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isOn = !isOn;
                    SE_audSource.Play();
                }
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
