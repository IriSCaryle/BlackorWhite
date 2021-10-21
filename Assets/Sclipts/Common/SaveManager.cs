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
    string filePath;//セーブデータのファイルパス

    public SaveData save = new SaveData();

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        filePath = Application.persistentDataPath + "/" + "Savedata.json";
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
        StreamWriter streamWriter = new StreamWriter(filePath,false);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    void Loading()//ロード
    {
        StreamReader streamReader;
        streamReader = new StreamReader(filePath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();

        save = JsonUtility.FromJson<SaveData>(data);
    }
    void Reseting()//リセット(現在デバッグ用)
    {
        File.Delete(filePath);
    }
}
