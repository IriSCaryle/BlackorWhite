using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator blurAnimator;
    [SerializeField] Animator menuAnimator;
    [SerializeField]bool isOpen;
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

    void CheckKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;

            CheckBool();
        }
    }

    void CheckBool()
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
    void StartAnimation()
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
