using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGMManager : MonoBehaviour
{

    static public BGMManager instance;
    [SerializeField] AudioSource aud_BGM;
    [SerializeField] List<AudioClip> BGM_list;
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
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        CheckNowScene(SceneManager.GetActiveScene());
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        Debug.Log(prevScene.name + "->" + nextScene.name);

        CheckNowScene(nextScene);
    }


    void CheckNowScene(Scene nextScene)
    {
        if (nextScene.name == "Stage1Scene" || nextScene.name == "Stage2Scene" || nextScene.name == "ExtraStage1Scene")
        {
            aud_BGM.clip = BGM_list[1];
            aud_BGM.Play();
        }
        else if (nextScene.name == "TitleScene" || nextScene.name == "SettingScene" || nextScene.name == "AchieveScene")
        {
            aud_BGM.clip = BGM_list[0];
            aud_BGM.Play();
        }
        else
        {
            Debug.LogError("該当するシーンのBGMがありません");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
