using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvertCoolTimeManager : MonoBehaviour
{
    [SerializeField] Image overRayImage;
    [SerializeField] PleyerSclipt playerScript;
    [SerializeField] float percentageAmount;
    [SerializeField] Animator animator;
    bool isCountDown;
    bool isAnimation;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        CallAnimation();
        isOpen = true;
    }
    private void Update()
    {
        if (percentageAmount == 0 && !isAnimation)
        {
            isAnimation = true;
            CallAnimation();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerScript.avility)
        {
            isCountDown = true;
            isAnimation = false;
            overRayImage.fillAmount = 1;
            isOpen = true;
        }
        ChangeAmount();
       
    }

    void ChangeAmount()
    {
        if (isCountDown)
        {
            percentageAmount = playerScript.nowCoolTime / playerScript.limitCoolTime;
            Debug.Log("CoolTime:" + percentageAmount * 100 + "%");
            overRayImage.fillAmount = percentageAmount;  
        }       
    }

    void CallAnimation()
    {
        Debug.Log("アニメーション");
        animator.SetTrigger("CanUse");
    }
    public void CanClose()
    {
        isOpen = false;
    }
    public void CanOpen()
    {
        isOpen = true;
    }
}
