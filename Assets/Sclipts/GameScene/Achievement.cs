using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{   [Header("実績名")]
    public string achieveName;
    [Header("解除条件"), TextArea(1, 6)]
    public string detail;
    public int ID;
    public bool isUnlock;

}
