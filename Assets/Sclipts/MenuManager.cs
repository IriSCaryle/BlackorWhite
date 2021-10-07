using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator blurAnimator;

    [SerializeField]bool isOpen;
    [SerializeField] bool isClose;
    // Start is called before the first frame update
    void Start()
    { 
        isOpen = false;
        isClose=false;
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
    }
    void StopAnination()
    {
        blurAnimator.SetTrigger("stop");
    }
}
