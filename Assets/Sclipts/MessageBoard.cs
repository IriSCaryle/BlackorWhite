using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class MessageBoard : MonoBehaviour
{
    
    [SerializeField] MessageList messageList;
    [SerializeField] Text text;

    [SerializeField] int messageID;

    [SerializeField] Animator messageAnimator;
    [SerializeField] Animator blurAnimator;
    GameObject target;
    bool isopend;
    bool isEnter;
    bool isExit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter && !isExit)
        {
            if (target.gameObject.tag == "Player")
            {

                if (Input.GetKeyDown(KeyCode.E) && isopend == false)
                {
                    OpenMessage();
                }
                else if (Input.GetKeyDown(KeyCode.E) && isopend == true)
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isExit = true;
        isEnter = false;
        target = null;
    }

    void OpenMessage()
    {


        GetMessage();
        blurAnimator.SetTrigger("start");
        isopend = true;
        messageAnimator.SetTrigger("start");
        
        Time.timeScale = 0;
        
    }
    void CloseMessage()
    {
     
        blurAnimator.SetTrigger("stop");
        messageAnimator.SetTrigger("stop");
        isopend = false;

        Time.timeScale = 1;

    }

    public void GetMessage()
    {
        Debug.Log("事前にメッセージを追加");
        text.text = messageList.messages[messageID].message;
    }
}
