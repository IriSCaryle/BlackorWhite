using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    string filePath;

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
        if (File.Exists(filePath))
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
        if (File.Exists(filePath))
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

    void Saving()
    { 
        string json = JsonUtility.ToJson(save);
        StreamWriter streamWriter = new StreamWriter(filePath,false);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    void Loading()
    {
        StreamReader streamReader;
        streamReader = new StreamReader(filePath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();

        save = JsonUtility.FromJson<SaveData>(data);
    }
    void Reseting()
    {
        File.Delete(filePath);
    }
}
