using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SettingDataManager : MonoBehaviour
{
    public SettingData settingData;

    [SerializeField] string filePath;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        filePath = Application.persistentDataPath + "/" + "Config.json";
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        if (!File.Exists(filePath))
        {
            settingData.resolutionX = Screen.currentResolution.width;
            settingData.resolutionY = Screen.currentResolution.height;
            switch(Screen.fullScreenMode){
                case FullScreenMode.FullScreenWindow:
                    settingData.windowType = 0;
                    break;
                case FullScreenMode.Windowed:
                    settingData.windowType = 1;
                    break;
                case FullScreenMode.MaximizedWindow:
                    settingData.windowType = 2;
                    break;
                default:
                    Debug.LogError("Setting:指定外のウィンドウモードです,フルスクリーンにします");
                    break;
            }
            settingData.bgmVolume = 100;
            settingData.seVolume = 100;

            Save();
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(settingData);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void Reset()
    {
        File.Delete(filePath);
    }
    public void Load()
    {
        StreamReader streamReader;
        streamReader = new StreamReader(filePath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();

        settingData = JsonUtility.FromJson<SettingData>(data);
    }
   
}
