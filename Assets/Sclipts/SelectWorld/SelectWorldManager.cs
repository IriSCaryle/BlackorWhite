using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWorldManager : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] Fade fade;
    [SerializeField] GameObject nowloadingText;

    [SerializeField] int nextStageNum;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        fade.FadeIn(1);
        SaveDataLoad();

    }

    void SaveDataLoad()
    {
        nowloadingText.SetActive(true);
        saveManager.Load();
        nextStageNum = saveManager.save.StageNum;

        switch (nextStageNum)
        {
            case 0:
                Debug.Log("ステージ0");
                nowloadingText.SetActive(false);
                fade.FadeOut(1, "ryuiScene");
                break;
            case 1:
                Debug.Log("ステージ1");
                nowloadingText.SetActive(false);
                fade.FadeOut(1, "Stage1Scene");

                break;
            default :
                Debug.Log("指定したステージがありません");
                break;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
