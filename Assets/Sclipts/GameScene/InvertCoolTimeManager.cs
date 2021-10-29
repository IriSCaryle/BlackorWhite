using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 反転動作のクールタイム動作     
/// </summary>
public class InvertCoolTimeManager : MonoBehaviour
{   [Header("タイマー進行度表示のオーバーレイ")]
    [SerializeField] Image overRayImage;
    [Header("プレイヤースクリプト")]
    [SerializeField] PleyerSclipt playerScript;
    [Header("進行度")]
    [SerializeField] float percentageAmount;
    [Header("アニメーター")]
    [SerializeField] Animator animator;
    bool isCountDown;
    bool isAnimation;
    [Header("表示されているか")]
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
        if (playerScript.avility)//Qキーを押したらクールダウンを数える
        {
            isCountDown = true;
            isAnimation = false;
            overRayImage.fillAmount = 1;
            isOpen = true;
        }
        ChangeAmount();
       
    }

    void ChangeAmount()//タイマー動作
    {
        if (isCountDown)
        {
            percentageAmount = playerScript.nowCoolTime / playerScript.limitCoolTime;
            Debug.Log("CoolTime:" + percentageAmount * 100 + "%");
            overRayImage.fillAmount = percentageAmount;  
        }       
    }

    void CallAnimation()//アニメーションを呼び出す
    {
        Debug.Log("アニメーション");
        animator.SetTrigger("CanUse");
    }
    public void CanClose()//非表示動作
    {
        isOpen = false;
    }
    public void CanOpen()//表示動作
    {
        isOpen = true;
    }
}
