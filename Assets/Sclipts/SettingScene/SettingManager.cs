using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 設定シーンの管理
/// </summary>
public class SettingManager : MonoBehaviour
{
    [Header("設定データ管理スクリプト")]
    [SerializeField] SettingDataManager settingDataManager;
    [Header("フェードスクリプト")]
    [SerializeField] Fade fade;
    [Header("適用ボタン")]
    [SerializeField] Button applyButton;
    [Header("各ドロップダウン")]
    [SerializeField] Dropdown resolusion;
    [SerializeField] Dropdown windowMode;
    [Header("各スライド")]
    [SerializeField] Slider bgm;
    [SerializeField] Slider se;
    [Header("Audio")]
    [SerializeField] AudioSource aud_SE;
    [SerializeField] AudioClip SE_decide;
    [SerializeField] AudioClip SE_cansel;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        settingDataManager = GameObject.FindGameObjectWithTag("SettingData").GetComponent<SettingDataManager>();
        LoadSettings();
        fade.FadeIn(1);
    }


    void LoadSettings()//現在の設定をスライダーやドロップダウンに適用
    {
        settingDataManager.Load();
        switch (settingDataManager.settingData.resolutionX)
        {
            case 1920:
                resolusion.value = 0;
                break;
            case 1280:
                resolusion.value = 1;
                break;
            case 1152:
                resolusion.value = 2;
                break;
            case 1024:
                resolusion.value = 3;
                break;
            default:
                Debug.Log("Setting:現在の解像度がリストにありません");
                
                break;
        }
        switch (settingDataManager.settingData.windowType)
        {
            case 0:
                windowMode.value = 0;
                break;
            case 1:
                windowMode.value = 1;
                break;
            case 2:
                windowMode.value = 2;
                break;
        }
        bgm.value = settingDataManager.settingData.bgmVolume;
        se.value = settingDataManager.settingData.seVolume;
    }

    public void OnValueChanged()//変更が加われた際に適用ボタンを表示する
    {
        if (!applyButton.gameObject.activeSelf)
        {
            applyButton.gameObject.SetActive(true);
        }
    }


    public void BGMVolChange()
    {
        settingDataManager.SetBGM(bgm.value);
    }
    public void SEVolChange()
    {
        settingDataManager.SetSE(se.value);
    }

    public void OnClickBack()//戻るボタン動作
    {
        aud_SE.clip = SE_cansel;
        aud_SE.Play();
        fade.FadeOut(1,"TitleScene");
    }

    public void OnClickApply()//適用ボタン動作
    {
        aud_SE.clip = SE_decide;
        aud_SE.Play();
        AssignResolution();
        AssignWindowMode();
        AssignBGM_SE();
        settingDataManager.Save();
        ChangeGraphic();
        applyButton.gameObject.SetActive(false);

        
    }

    void ChangeGraphic()
    {
        switch (settingDataManager.settingData.windowType)
        {
            case 0:
                Screen.SetResolution(settingDataManager.settingData.resolutionX, settingDataManager.settingData.resolutionY,FullScreenMode.FullScreenWindow,60);
                break;
            case 1:
                Screen.SetResolution(settingDataManager.settingData.resolutionX, settingDataManager.settingData.resolutionY, FullScreenMode.Windowed, 60);
                break;
            case 2:
                Screen.SetResolution(settingDataManager.settingData.resolutionX, settingDataManager.settingData.resolutionY, FullScreenMode.MaximizedWindow, 60);
                break;
        }
        
    }
    void AssignResolution()//設定データに解像度を設定
    {
        switch (resolusion.value)
        {
            case 0:
                settingDataManager.settingData.resolutionX = 1920;
                settingDataManager.settingData.resolutionY = 1080;
                break;
            case 1:
                settingDataManager.settingData.resolutionX = 1280;
                settingDataManager.settingData.resolutionY = 720;
                break;
            case 2:
                settingDataManager.settingData.resolutionX = 1152;
                settingDataManager.settingData.resolutionY = 648;
                break;
            case 3:
                settingDataManager.settingData.resolutionX = 1024;
                settingDataManager.settingData.resolutionY = 576;
                break;
        }
    }

    void AssignWindowMode()//画面タイプを設定データに設定
    {
        switch (windowMode.value)
        {
            case 0:
                settingDataManager.settingData.windowType = 0;
  
                break;
            case 1:
                settingDataManager.settingData.windowType = 1;
                break;
            case 2:
                settingDataManager.settingData.windowType = 2;
                break;
        }
    }

    void AssignBGM_SE()//音量を設定データに設定
    {
        settingDataManager.settingData.bgmVolume = bgm.value;
        settingDataManager.settingData.seVolume = se.value;
    }

 
   


    
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
