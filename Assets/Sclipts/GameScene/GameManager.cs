using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ゲーム(ステージ)の管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("プレイヤーが出現する位置")]
    [SerializeField] GameObject SpawnPoint;
    [Header("ステージ番号")]
    [SerializeField] int StageNum;
    [Header("ステージクリア実績ID")]
    [SerializeField] int ID;
    [Header("セーブ管理スクリプト")]
    [SerializeField] SaveManager saveManager;
    [Header("プレイヤー")]
    [SerializeField] GameObject player;
    [Header("プレイヤースクリプト")]
    [SerializeField] PleyerSclipt playerSclipt;
    [Header("フェードスクリプト")]
    [SerializeField] Fade fade;
    [Header("ブラーアニメーター")]
    [SerializeField] Animator blueAnimator;
    [Header("ゴールアニメーター")]
    [SerializeField] Animator goalAnimator;
    [Header("エクストラステージのポップ")]
    [SerializeField] GameObject extraStageEnablePopUp;
    [Header("アチーブメント関連")]

    [SerializeField] Animator achievementAnimator;

    [SerializeField] Text achievementTitleText;
    [SerializeField] Text achievementDetailText;

    public bool isActiveAchievements;

    [SerializeField]List<int> AchievementAnimReserve;

    [Header("シューター")]
    [SerializeField] Shooter shooter;

    [Header("Audio")]
    [SerializeField] AudioSource aud_SE;
    [SerializeField] AudioClip SE_negative;
    [SerializeField] AudioClip SE_positive;
    [SerializeField] AudioClip SE_achievement;
    [SerializeField] AudioClip SE_goal;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        playerSclipt = player.GetComponent<PleyerSclipt>();
        CheckSave();
        isActiveAchievements = true;
        if (PlayerPrefs.GetInt("Load?")==1)//コンティニューを選択した場合セーブデータをロードします
        {
            Debug.Log("Save:Continueなのでロードします");
            LoadingSaveData();
        }
        
    }
    private void Start()
    {
        fade.FadeIn(1);
    }


    void CheckSave()//セーブがあるかチェックします("デバッグ用")
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
    void LoadingSaveData()//セーブデータをロードしプレイヤーの位置を同期します
    {
        saveManager.Load();
        if(saveManager.save.PlayerPos.x ==0 && saveManager.save.PlayerPos.y == 0&& saveManager.save.PlayerPos.z == 0)
        {
            player.transform.position = SpawnPoint.transform.position;
        }
        else
        {
            player.transform.position = saveManager.save.PlayerPos;
        }
        
        
    }

    void SaveData()//プレイヤーの位置とステージ番号をセーブします
    {
        Debug.Log("SavingPlayerPos:"+ player.transform.position);
        saveManager.save.PlayerPos = player.transform.position;
        saveManager.save.StageNum = StageNum;
        saveManager.Save();
    }

    public void OnClickBackToMenu()//タイトルメニューに戻る動作
    {
        aud_SE.clip = SE_positive;
        aud_SE.Play();
        Time.timeScale = 1;
        SaveData();
        fade.FadeOut(1,"TitleScene");
    }

    public void GameClearBackToMenu()
    {
        aud_SE.clip = SE_positive;
        aud_SE.Play();
        Time.timeScale = 1;
        fade.FadeOut(1, "TitleScene");
    }

    public void GameOverBacktoTitle()
    {
        aud_SE.clip = SE_positive;
        aud_SE.Play();
        saveManager.save.PlayerPos = SpawnPoint.transform.position;
        saveManager.save.StageNum = StageNum;
        saveManager.Save();
        Time.timeScale = 1;
        fade.FadeOut(1, "TitleScene");
    }

   
    public void Retry()//リトライの処理
    {
        aud_SE.clip = SE_positive;
        aud_SE.Play();
        saveManager.save.PlayerPos = SpawnPoint.transform.position;
        saveManager.save.StageNum = StageNum;
        saveManager.Save();

        Time.timeScale = 1;
        fade.FadeOut(1, "SelectWorldScene");
    }

    public void PlayerLazerDead()//レーザーがプレイヤーを検知したことをプレイヤーに知らせます
    {
        playerSclipt.PlayerLazerDead();
    }

    public void StageGoal()//プレイヤーのゴールしたことを知らせステージ番号を次に設定しゴール演出を再生します
    {
        playerSclipt.PlayerGoal();
        if (saveManager != null)
        {
            saveManager.save.StageNum = StageNum+1 ;
            saveManager.save.PlayerPos.x = 0;
            saveManager.save.PlayerPos.y = 0;
            saveManager.save.PlayerPos.z = 0;
            saveManager.Save();
            
            StartCoroutine("Goal");
          
        }

        GoalCheckAchievement();


    }

    IEnumerator Goal()//ゴール演出
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        blueAnimator.SetTrigger("start");
        goalAnimator.SetTrigger("Goal");
        
        yield return StartCoroutine(ExtraStage());
       
    }

    IEnumerator ExtraStage()
    {
        if (saveManager.maxStage < saveManager.save.StageNum)
        {
            CheckExtraStage();
            yield return new WaitForSecondsRealtime(4);
            GameClearBackToMenu();
        }
        yield break;
    }

    void CheckExtraStage()
    {        
            Debug.Log("エキストラ出現");
            extraStageEnablePopUp.SetActive(true); //エクストラステージの出現を知らせる
    }

    

    public void OnClickNextStage()//次のステージへ行く動作
    {
        Time.timeScale = 1;
       
        fade.FadeOut(1, "SelectWorldScene");
    }
    

    void GoalCheckAchievement()
    {
        CheckAchievement(ID);
        ShooterAchievement();
    }
  

    void ShooterAchievement()//シューターの実績
    {
        bool isclear;
        int num;
        (isclear, num) = shooter.AchievementClear();

        if (isclear)
        {
            CheckAchievement(num);
        }
    }

    public void CheckAchievement(int achieveID)//指定されたidの実績をセーブから探しアンロックする
    {
        for (int i = 0; i < saveManager.save.achivements.Length; i++)
        {
            if (!saveManager.save.achivements[i].isUnlock)
            {
                if (saveManager.save.achivements[i].ID == achieveID)
                {
                    saveManager.save.achivements[i].isUnlock = true;
                    saveManager.Save();
                    CheckAllAchievementClear();
                    AchievementReserve(i);
                }
            }
        }
    }

    void AchievementReserve(int id)
    {
        AchievementAnimReserve.Add(id);
    }

    bool CheckAchieveAnimList()
    {
        if (AchievementAnimReserve.Count != 0)
        {
            AchievementAnimPlay(AchievementAnimReserve[0]);
            AchievementAnimReserve.RemoveAt(0);

            return true;
        }
        else
        {
            return false;
        }
    }

    void AchievementAnimPlay(int id)
    {
        AchievementAnimation(id);
    }

    private void Update()
    {
        if (isActiveAchievements)
        {
            if (CheckAchieveAnimList())
            {
                isActiveAchievements = false;
            }
            else
            {
                isActiveAchievements = true;
            }
        }
    }

    void AchievementAnimation(int length)//アチーブメント解除のアニメーションを実行
    {
        achievementTitleText.text = saveManager.save.achivements[length].achieveName;
        achievementDetailText.text = saveManager.save.achivements[length].detail;
        aud_SE.clip = SE_achievement;
        aud_SE.Play();
        achievementAnimator.SetTrigger("open");
        isActiveAchievements = false;
    }

    void CheckAllAchievementClear()
    {
        int max = saveManager.save.achivements.Length-1;
        int clear = 0;
        for (int i = 0; i < saveManager.save.achivements.Length; i++)
        {
            if (saveManager.save.achivements[i].isUnlock)
            {
                clear += 1;
            }
        }
        if(clear == max)
        {
            CheckAchievement(6);
        }
    }

  
 
}
