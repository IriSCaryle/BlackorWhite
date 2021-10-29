using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("各アニメーター")]
    [SerializeField] Animator blurAnimator;
    [SerializeField] Animator menuAnimator;
    [Header("メニューが開かれているか")]
    [SerializeField]bool isOpen;
    [Header("プレイヤースクリプト")]
    [SerializeField] PleyerSclipt pleyerSclipt;
    // Start is called before the first frame update
    void Start()
    { 
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckKey();
    }

    void CheckKey()//キーを検出
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;

            CheckBool();
        }
    }

    void CheckBool()//isOpenを監視する関数
    {
        if (isOpen)
        {
            pleyerSclipt.freeze = true;
            StartAnimation();
        }
        else
        {
            pleyerSclipt.freeze = false;
            StopAnination();
        }
    }
    void StartAnimation()//アニメーション監視関数
    {
        blurAnimator.SetTrigger("start");
        menuAnimator.SetTrigger("start");
        Time.timeScale = 0;
    }
    void StopAnination()
    {
        blurAnimator.SetTrigger("stop");
        menuAnimator.SetTrigger("stop");
        Time.timeScale = 1;
    }

    public void OnStop()
    {
        isOpen = false;
        CheckBool();
    }
}
