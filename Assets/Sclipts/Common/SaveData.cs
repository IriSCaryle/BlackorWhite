using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// セーブデータの構造
/// </summary>
[System.Serializable]
public class SaveData
{
    public int StageNum;

    public Vector3 PlayerPos;

    public Achievement[] achivements ;

}
