using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{

    [SerializeField] Fade fade;
    [SerializeField] SaveManager saveManager;
    [SerializeField] Button continueButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if (saveManager.Load()){
            continueButton.interactable = true;
            //--デバッグ用--//
            //saveManager.SaveDataReset();
        }
        else
        {
            Text[] texts = continueButton.gameObject.GetComponentsInChildren<Text>();
            Debug.Log(texts[0]);
            Color color = texts[0].color;
            color = new Color(0.88f, 0.88f, 0.88f, 1);
            texts[0].color = color;
            texts[1].color = color;
            continueButton.interactable = false;
        }

        fade.FadeIn(1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        
        fade.FadeOut(1,"ryuiScene");
        PlayerPrefs.SetInt("Load?", 0);
        PlayerPrefs.Save();
    }
    public void OnClickContinue()
    {
        saveManager.Load();
        fade.FadeOut(1,"SelectWorldScene");
        PlayerPrefs.SetInt("Load?",1);
        PlayerPrefs.Save();
    }
    public void OnClickSetting()
    {

    }
    
    public void _OnClickReset()
    {
        saveManager.SaveDataReset();
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
