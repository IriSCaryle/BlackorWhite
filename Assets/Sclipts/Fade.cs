using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    bool isFadein;
    bool isFadeout;
    float fadetime;
    float alpha;
  
    
    // Start is called before the first frame update
    void Start()
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
                
            }
            FadeImage.color = new Color(0, 0, 0, alpha);
        }
    }
    public void FadeIn(float time)
    {
        FadeImage.gameObject.SetActive(true);
        fadetime = time;
        alpha = 1;
        
        isFadein = true;
        Debug.Log("フェードイン開始" );
        Debug.Log("初期:" + alpha);
       
    }
    public void FadeOut(float time)
    {
        FadeImage.gameObject.SetActive(true);
        fadetime = time;
        alpha = 0;
        isFadeout = true;
        Debug.Log("フェードアウト開始" );
        Debug.Log("初期:" + alpha);
    }
    
}
