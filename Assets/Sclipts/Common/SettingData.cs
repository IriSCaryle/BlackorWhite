using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 設定項目の構造
/// </summary>
[System.Serializable]
public class SettingData
{
    public int resolutionX;//解像度X軸
    public int resolutionY;//解像度Y軸

    public int windowType;//0:FullScreen,1:Window,2:Borderless

    public float bgmVolume;//BGM

    public float seVolume;//SE
}
