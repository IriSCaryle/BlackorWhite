using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
/// <summary>
/// メッセージボード   
/// </summary>
public class MessageBoard : MonoBehaviour
{
    [Header("メッセージリスト")]
    [SerializeField] MessageList messageList;
    [Header("メッセージテキスト")]
    [SerializeField] Text text;
    [Header("メッセージID")]
    [SerializeField] int messageID;//MessageList.messagesの要素数を指定
    [Header("各アニメーター")]
    [SerializeField] Animator messageAnimator;
    [SerializeField] Animator blurAnimator;
    [Header("UI")]
    [SerializeField] PopUpUI popUpUI;
    [Header("AudioSource")]
    [SerializeField] AudioSource SE_audSource;
    GameObject target;
    bool isopend;
    bool isEnter;
    bool isExit;
    [Header("プレイヤースクリプト")]
    [SerializeField] PleyerSclipt pleyerSclipt;

    [SerializeField] AudioClip SE_open;
    [SerializeField] AudioClip SE_close;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter && !isExit)//範囲内にいるのがプレイヤーだった場合にメッセージを表示する動作
        {
            if (target.gameObject.tag == "Player")
            {

                if (Input.GetKeyDown(KeyCode.E) && isopend == false)
                {
                    OpenMessage();
                }
                else if (Input.GetKeyDown(KeyCode.E)&& isopend == true)
                {
                    CloseMessage();
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEnter = true;
        isExit = false;
        target = collision.gameObject;
        popUpUI.isFadeIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isExit = true;
        isEnter = false;
        target = null;
        popUpUI.isFadeOut = true;
    }

    void OpenMessage()//メッセージを表示する動作
    {
        pleyerSclipt.freeze = true;
        GetMessage();
        SE_audSource.clip = SE_open;
        SE_audSource.Play();
        blurAnimator.SetTrigger("start");
        isopend = true;
        messageAnimator.SetTrigger("start");
        
        Time.timeScale = 0;
        
    }
    void CloseMessage()//メッセージを非表示にする動作
    {
        SE_audSource.clip = SE_close;
        SE_audSource.Play();
        blurAnimator.SetTrigger("stop");
        messageAnimator.SetTrigger("stop");
        isopend = false;

        Time.timeScale = 1;

        Invoke("unfreezePlayer",0.1f);
       

    }

    void unfreezePlayer()
    {
        pleyerSclipt.freeze = false;
    }

    public void GetMessage()//メッセージリストからメッセージを取得する関数
    {
        Debug.Log("事前にメッセージを追加");
        text.text = messageList.messages[messageID].message;
    }
}
