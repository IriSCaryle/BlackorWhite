using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievesManager : MonoBehaviour
{
    [Header("セーブデータ")]
    [SerializeField] SaveManager saveManager;
    [Header("実績データ関連")]
    [SerializeField] AchievementManager achievementManager;
    [SerializeField] GameObject AchievementContent;
    [SerializeField] Transform contentsParent;
    [Header("フェード")]
    [SerializeField] Fade fade;
    [Header("全体達成度関連")]
    [SerializeField] Image parsentSlider;
    [SerializeField] Text parsentText;

    [SerializeField]int achiebvementUnlockNum;
    // Start is called before the first frame update
    void Start()
    {
        GameObject saveManagerObject = GameObject.FindGameObjectWithTag("SaveManager");
        saveManager = saveManagerObject.GetComponent<SaveManager>();
        achievementManager = saveManagerObject.GetComponent<AchievementManager>();
        CheckAllAchievements();
        CheckAchievementDegrees();
        fade.FadeIn(1);
    }

    void CheckAllAchievements()
    {
        foreach(Achievement ACHIEVEMENT in achievementManager.DefaultAchievements)
        {
            GameObject achievement = Instantiate(AchievementContent, contentsParent);
            achievement.SetActive(true);
            Text[] texts = achievement.GetComponentsInChildren<Text>();
            Debug.Log("NameText" + texts[0].name);
            Debug.Log("DetailText" + texts[1].name);

            texts[0].text = ACHIEVEMENT.achieveName;
            texts[1].text = ACHIEVEMENT.detail;

            Achievement saveDataAchievement = SerchSaveDataAchieves(ACHIEVEMENT.ID);//セーブデータに入っている該当のアチーブメントを取得

            if (!saveDataAchievement.isUnlock)
            {
                Debug.Log("実績未解除を発見");
                Image[] image = achievement.GetComponentsInChildren<Image>();
                Debug.Log("Image:" + image[1].name);
                Color tmpColor = image[1].color;
                tmpColor.a = 0.44f;
                image[1].color = tmpColor;
            }
            else
            {
                achiebvementUnlockNum += 1;
            }
        }
    }


    void CheckAchievementDegrees()
    {
        Debug.Log("UnlockNum:"+achiebvementUnlockNum+",AchievementsNum:"+achievementManager.DefaultAchievements.Count);
        int achievementsNum = achievementManager.DefaultAchievements.Count;
        float parsent = (float)achiebvementUnlockNum / (float)achievementsNum;

        parsentSlider.fillAmount = parsent;
        Debug.Log("達成度少数:" + parsent);
        double Parsent = (System.Math.Truncate((double)parsent * 100.0)) / 100;
        Debug.Log("達成度:" + Parsent);
        parsentText.text = (Parsent*100).ToString() +"%";
    }
    Achievement SerchSaveDataAchieves(int i )
    {
       foreach(Achievement tmp in saveManager.save.achivements)
        {
            if(tmp.ID == i)
            {
                Debug.Log("セーブデータをはっけｎ");
                return tmp;
            }
        }

        Debug.LogError("アチーブメントに存在しない実績がセーブデータに保存されています、今すぐ管理者に問い合わせてください");
        return null;
    }


    public void OnClickBack()//戻るボタン動作
    {
        fade.FadeOut(1, "TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
