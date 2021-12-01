using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class TitleManager : MonoBehaviour
{
    [Header("フェードスクリプト")]
    [SerializeField] Fade fade;
    [Header("セーブ管理スクリプト")]
    [SerializeField] SaveManager saveManager;
    [Header("設定管理スクリプト")]
    [SerializeField] SettingDataManager settingDataManager;
    [Header("コンティニューボタン")]
    [SerializeField] Button continueButton;
    [Header("エキストラボタン")]
    [SerializeField] Button extraStageButton;
    [Header("デバッグボタン(ryuiSceneへ移動したい際に使用)")]
    [SerializeField] bool isDebug;
    [SerializeField] GameObject extraInfoPop;
    [Header("AudioSource")]
    [SerializeField] AudioSource SE_audSource;
    [SerializeField] AudioSource BGM_audSource;
    [Header("AudioClip")]
    [SerializeField] AudioClip SE_decide;
    [SerializeField] AudioClip SE_cansel;
  

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        if (saveManager.Load()){//セーブデータをロード
            continueButton.interactable = true;
            //--デバッグ用--//
            //saveManager.SaveDataReset();
        }
        else
        {
            //設定がない場合continueボタンを非表示
            DisableContinueButton();
            DisableExtraButton();
        }

        if(saveManager.save.StageNum <=saveManager.maxStage)
        {
            DisableExtraButton();
        }
        else if(saveManager.save.StageNum == saveManager.maxStage+1)
        {
            extraInfoPop.SetActive(true);

            Invoke("DisableInfoPop",5);
        }


       

        fade.FadeIn(1);

    }

  
    void DisableInfoPop()
    {
        extraInfoPop.SetActive(false);
    }
    void DisableExtraButton()
    {
        extraStageButton.gameObject.SetActive(false);
    }

    void DisableContinueButton()
    {
        Text[] texts = continueButton.gameObject.GetComponentsInChildren<Text>();
        Debug.Log(texts[0]);
        Color color = texts[0].color;
        color = new Color(0.88f, 0.88f, 0.88f, 1);
        texts[0].color = color;
        texts[1].color = color;
        continueButton.interactable = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void DecidedSE()
    {
        SE_audSource.clip = SE_decide;
        SE_audSource.Play();
    }

    public void CanselSE()
    {
        SE_audSource.clip = SE_cansel;
        SE_audSource.Play();
    }
    public void OnClickExtraStart()
    {
        DecidedSE();
        //fade.FadeOut(1, "ExtraStage1");
        PlayerPrefs.SetInt("Load?", 0);
        PlayerPrefs.Save();
    }

    public void OnClickAchieve()
    {
        DecidedSE();
        fade.FadeOut(1, "AchieveScene");
    }

    public void OnClickStart()//スタートボタン動作
    {
        if (isDebug)
        {
            DecidedSE();
            saveManager.save.StageNum = 1;
            saveManager.Save();
            fade.FadeOut(1, "ryuiScene");
            PlayerPrefs.SetInt("Load?", 0);
            PlayerPrefs.Save();
        }
        else
        {
            DecidedSE(); 
            PlayerPrefs.SetInt("Load?", 0);
            PlayerPrefs.Save();
            saveManager.save.StageNum = 1;
            saveManager.Save();
            fade.FadeOut(1, "Stage1Scene");

        }
    }
    public void OnClickContinue()//コンティニューボタン動作
    {
        if (isDebug)//デバッグ使用時
        {
            saveManager.save.StageNum = 0;
            saveManager.Save();
        }
        DecidedSE();
        saveManager.Load();
        fade.FadeOut(1,"SelectWorldScene");
        PlayerPrefs.SetInt("Load?",1);
        PlayerPrefs.Save();
    }
    public void OnClickSetting()//設定ボタン動作
    {
        DecidedSE();
        fade.FadeOut(1, "SettingScene");
    }
    
    public void _OnClickReset()//セーブデータ削除ボダン動作
    {
        DecidedSE();
        saveManager.SaveDataReset();
        settingDataManager.Reset();
    }
    public void OnClickQuit()//終了ボタン動作
    {
        DecidedSE();
        Application.Quit();
    }
}
