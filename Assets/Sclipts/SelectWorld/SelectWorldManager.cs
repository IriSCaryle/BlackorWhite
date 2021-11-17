using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// セレクトワールドシーンを管理するスクリプト
/// </summary>
public class SelectWorldManager : MonoBehaviour
{   [Header("セーブスクリプト")]
    [SerializeField] SaveManager saveManager;
    [Header("フェードスクリプト")]
    [SerializeField] Fade fade;
    [Header("NowLoadingオブジェクト")]
    [SerializeField] GameObject nowloadingText;
    [Header("次のステージ番号")]
    [SerializeField] int nextStageNum;

    [Header("最大ステージ数")]

    [SerializeField] int maxStageNum;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        fade.FadeIn(1);
        SaveDataLoad();

    }

    void SaveDataLoad()//セーブ管理スクリプトからセーブをロードしステージ番号を取得しステージ番号に応じたシーンに遷移する
    {
        nowloadingText.SetActive(true);
        saveManager.Load();
        nextStageNum = saveManager.save.StageNum;
        maxStageNum = saveManager.maxStage;
        switch (nextStageNum)//ノーマルステージ
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
            case 2:
                Debug.Log("ステージ2");
                nowloadingText.SetActive(false);
                fade.FadeOut(1, "Stage2Scene");
                break;

            default :
                Debug.Log("指定したステージがありません");
                break;

        }

       

        if (saveManager.maxStage < nextStageNum)
        {
            Debug.Log("エクストラステージに移行します");
            switch (nextStageNum)
            {
                case 3:
                    nowloadingText.SetActive(false);
                    fade.FadeOut(1, "ExtraStage1Scene");
                    break;

                default :
                    Debug.Log("ステージが設定されていません管理者に問い合わせてね");
                    break;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
