using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Audio;
/// <summary>
/// 設定データの管理
/// </summary>
public class SettingDataManager : MonoBehaviour
{
    public static SettingDataManager instance;

    public SettingData settingData;

    [SerializeField] string filePath;//ファイルパス
    [SerializeField] AudioMixer audioMixer;
    const float AUDIO_DEFAULT_VOL = 0f;
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
            settingData.bgmVolume = AUDIO_DEFAULT_VOL;
            settingData.seVolume = AUDIO_DEFAULT_VOL;
            SetAudioVolme();

            Save();
        }
        else
        {
            Load();

            SetAudioVolme();

        }
    }

    public void SetBGM(float bgm)
    {
        audioMixer.SetFloat("BGM",bgm);
        settingData.bgmVolume = bgm;
    }
    public void SetSE(float se)
    {
        audioMixer.SetFloat("SE",se);
        settingData.seVolume =se;
    }



    void SetAudioVolme()
    {
        
        audioMixer.SetFloat("BGM", settingData.bgmVolume);
        audioMixer.SetFloat("SE", settingData.seVolume);

       
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
        settingData = new SettingData();
        settingData.seVolume = AUDIO_DEFAULT_VOL;
        settingData.bgmVolume = AUDIO_DEFAULT_VOL;

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
