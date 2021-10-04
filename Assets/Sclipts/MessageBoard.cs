using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageBoard : MonoBehaviour
{
    [Header("メッセージ"), SerializeField, TextArea(1, 6)]
    string message;
    [SerializeField] BlurChanger blur;
    [SerializeField] MessageList messageList;
    [SerializeField] Text text;

    [SerializeField] int messageID;


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
        target = null;
    }

    void OpenMessage()
    {
        if (text.text == null)
        {
            Debug.Log("事前にメッセージを追加");
            text.text = messageList.messages[messageID].message;
        }   
        blur.OnBlur();
        isopend = true;
    }
    void CloseMessage()
    {
        blur.OnNormal();
        isopend = false;
        isEnter = false;

    }
}
