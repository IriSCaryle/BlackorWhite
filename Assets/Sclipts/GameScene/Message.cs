using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    [Header("メッセージ"), SerializeField, TextArea(1, 6)]
    public string message;

    
}
