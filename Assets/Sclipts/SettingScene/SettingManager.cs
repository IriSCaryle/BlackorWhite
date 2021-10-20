﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingManager : MonoBehaviour
{
    [SerializeField] SettingDataManager settingDataManager;
    [SerializeField] Fade fade;
    [SerializeField] Button applyButton;
    [SerializeField] Dropdown resolusion;
    [SerializeField] Dropdown windowMode;
    [SerializeField] Slider bgm;
    [SerializeField] Slider se;
    // Start is called before the first frame update
    void Start()
    {
        settingDataManager = GameObject.FindGameObjectWithTag("SettingData").GetComponent<SettingDataManager>();
        LoadSettings();
        fade.FadeIn(1);
    }


    void LoadSettings()
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

    public void OnValueChanged()
    {
        if (!applyButton.gameObject.activeSelf)
        {
            applyButton.gameObject.SetActive(true);
        }
    }

    public void OnClickBack()
    {
        fade.FadeOut(1,"TitleScene");
    }

    public void OnClickApply()
    {
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
    void AssignResolution()
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

    void AssignWindowMode()
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

    void AssignBGM_SE()
    {
        settingDataManager.settingData.bgmVolume = bgm.value;
        settingDataManager.settingData.seVolume = se.value;
    }

 
    public void OnChangeBGM()
    {
        //未割当
    }

    public void OnChangeSE()
    {

    }


    
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
