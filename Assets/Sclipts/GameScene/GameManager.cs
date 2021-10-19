using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] GameObject player;
    [SerializeField] Fade fade;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();

        CheckSave();

        if (PlayerPrefs.GetInt("Load?")==1)
        {
            Debug.Log("Save:Continueなのでロードします");
            LoadingSaveData();
        }
        
    }
    private void Start()
    {
        fade.FadeIn(1);
    }


    void CheckSave()
    {
        if (saveManager.Load())
        {
            Debug.Log("Save:あります");
        }
        else
        {
            Debug.Log("Save:ありません");
        }
    }
    void LoadingSaveData()
    {
        saveManager.Load();
        player.transform.position = saveManager.save.PlayerPos;
        
    }

    void SaveData()
    {
        saveManager.save.PlayerPos = player.transform.position;
        saveManager.save.StageNum = 1;
        saveManager.Save();
    }

    public void OnClickBackToMenu()
    {
        SaveData();
        Time.timeScale = 1;
        fade.FadeOut(1,"TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
