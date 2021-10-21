using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] int StageNum;
    [SerializeField] SaveManager saveManager;
    [SerializeField] GameObject player;
    [SerializeField] PleyerSclipt playerSclipt;
    [SerializeField] Fade fade;
    [SerializeField] Animator blueAnimator;
    [SerializeField] Animator goalAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        playerSclipt = player.GetComponent<PleyerSclipt>();
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
        saveManager.save.StageNum = StageNum;
        saveManager.Save();
    }

    public void OnClickBackToMenu()
    {
        Time.timeScale = 1;
        SaveData();
        fade.FadeOut(1,"TitleScene");
    }

    public void Retry()
    {
        saveManager.save.PlayerPos = SpawnPoint.transform.position;
        saveManager.save.StageNum = StageNum;
        saveManager.Save();
        Time.timeScale = 1;
        fade.FadeOut(1, "SelectWorldScene");
    }

    public void PlayerLazerDead()
    {
        playerSclipt.PlayerLazerDead();
    }

    public void StageGoal()
    {
        playerSclipt.PlayerGoal();
        
        if (saveManager != null)
        { 
            saveManager.save.StageNum += 1;
            saveManager.Save();
        }

        StartCoroutine("Goal");
    }

    IEnumerator Goal()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        blueAnimator.SetTrigger("start");
        goalAnimator.SetTrigger("Goal");
        yield break;
    }

    public void OnClickNextStage()
    {
        Time.timeScale = 1;
        fade.FadeOut(1, "SelectWorldScene");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
