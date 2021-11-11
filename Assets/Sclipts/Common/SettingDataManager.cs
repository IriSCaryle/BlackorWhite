using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// 設定データの管理
/// </summary>
public class SettingDataManager : MonoBehaviour
{
    public static SettingDataManager instance;

    public SettingData settingData;

    [SerializeField] string filePath;//ファイルパス
    // Start is called before the first frame update
    void Awake()
    {
        if (instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        filePath = Application.persistentDataPath + "/" + "Config.json";
    }

    private void Start()
    {
        Init();
    }

    void Init()//設定の初期化
    {
        if (!File.Exists(filePath))//ファイルが存在しない場合 現在の解像度+現在の画面モード,音量maxで保存
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

    public void Save()//セーブ
    {
        string json = JsonUtility.ToJson(settingData);
        StreamWriter streamWriter = new StreamWriter(filePath,false);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void Reset()//リセット
    {
        File.Delete(filePath);
    }
    public void Load()//ロード
    {
        StreamReader streamReader;
        streamReader = new StreamReader(filePath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();

        settingData = JsonUtility.FromJson<SettingData>(data);
    }
   
}
