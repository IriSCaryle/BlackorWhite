using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// セーブデータ管理
/// </summary>
public class SaveManager : MonoBehaviour
{
    static public SaveManager instance;

    string filePath;//セーブデータのファイルパス

    public SaveData save = new SaveData();

    [SerializeField] AchievementManager achievement;


    [Header("ノーマルステージ最大ステージ数")]
    public int maxStage;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        filePath = Application.persistentDataPath + "/" + "Savedata.json";
        if (!Load())
        {
            save.achivements = achievement.DefaultAchievements.ToArray();
        }
    }

    public bool Save()
    {

        Saving();
        return true;
      
    }

    public bool Load()
    {
        if (File.Exists(filePath))//ファイルの存在確認
        {
            Loading();
            return true;
            
        }
        else
        {
            Debug.LogWarning("セーブデータが存在しません");
            return false;
        }
    }

    public bool SaveDataReset()
    {
        if (File.Exists(filePath))//ファイルの存在確認
        {
            Reseting();
            return true;

        }
        else
        {
            Debug.LogWarning("セーブデータが存在しません");
            return true;
        }
    }

    void Saving()//セーブ
    { 
        string json = JsonUtility.ToJson(save);
        Debug.Log("[JsonDataSaving]\n" + json);
        StreamWriter streamWriter = new StreamWriter(filePath,false);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void DebugJson()
    {
        string json = JsonUtility.ToJson(save);
        Debug.Log("[JsonDataSaving]\n" + json);
    }

    void Loading()//ロード
    {
        StreamReader streamReader;
        streamReader = new StreamReader(filePath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();
        Debug.Log("[JsonDataLoading]\n" + data);
        save = JsonUtility.FromJson<SaveData>(data);
    }
    void Reseting()//リセット(現在デバッグ用)
    {
        File.Delete(filePath);
        save = new SaveData();
        save.achivements = achievement.DefaultAchievements.ToArray();

    }
}
