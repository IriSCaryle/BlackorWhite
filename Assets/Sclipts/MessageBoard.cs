using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageBoard : MonoBehaviour
{
    [Header("メッセージ"), SerializeField,TextArea(1,6)]
    string message;
    [SerializeField] BlurChanger blur;
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Debug.Log("操作ポップアップしています");
        }
    }
}
