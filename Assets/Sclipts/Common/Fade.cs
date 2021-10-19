using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Fade : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    [SerializeField]bool isFadein;
    [SerializeField]bool isFadeout;
    float fadetime;
    [SerializeField]float alpha;

    string nextSceneName;
    // Start is called before the first frame update
    void Awake()
    {
        fadetime = 0;
        isFadein = false;
        isFadeout = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFadein)
        {
             alpha -= Time.deltaTime / fadetime;
            Debug.Log(alpha +"1f" + Time.deltaTime);
            if (alpha <= 0) 
            {
                isFadein = false;
                Debug.Log("フェード終了");
                FadeImage.gameObject.SetActive(false);
               
            }
            FadeImage.color = new Color(0, 0, 0, alpha);
        }
        if (isFadeout)
        {
            alpha += Time.deltaTime / fadetime;
            Debug.Log(alpha);
            if (alpha >= 1)
            {
                isFadeout = false;
                Debug.Log("フェード終了");
                if (nextSceneName != "")
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogWarning("シーンネームがありません現状維持します");
                }
            }
            FadeImage.color = new Color(0, 0, 0, alpha);
        }
    }
    public void FadeIn(float time)
    {
        alpha = 1;
        FadeImage.color = Color.black;
        FadeImage.gameObject.SetActive(true);
        
        fadetime = time;
        isFadein = true;
        Debug.Log("フェードイン開始" );
        Debug.Log("初期:" + alpha);
       
    }
    public void FadeOut(float time,string sceneName)
    {
        alpha = 0;
        FadeImage.gameObject.SetActive(true);
        fadetime = time;
        
        nextSceneName = sceneName;
        isFadeout = true;
        Debug.Log("フェードアウト開始" );
        Debug.Log("初期:" + alpha);
    }
    
}
