using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator blurAnimator;
    [SerializeField] Animator menuAnimator;
    [SerializeField]bool isOpen;

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
            StartAnimation();
        }
        else
        {
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
}
