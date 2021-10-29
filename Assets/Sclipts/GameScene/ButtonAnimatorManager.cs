using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// メニューのボタンアニメーション
/// </summary>
public class ButtonAnimatorManager : MonoBehaviour
{
    [SerializeField] Animator menuTextAnimator;
    

    public void OnNormal()
    {
        menuTextAnimator.SetTrigger("normal");
    }
    public void OnHightlight()
    {
        menuTextAnimator.SetTrigger("hightlight");
    }
    public void OnPress()
    {
        menuTextAnimator.SetTrigger("press");
    }
    public void OnSelect()
    {
        menuTextAnimator.SetTrigger("select");
    }

    public void OnDisabled()
    {
        menuTextAnimator.SetTrigger("disable");
    }

    public void OnNormal2()
    {
        menuTextAnimator.SetTrigger("normal2");
    }
    public void OnHightlight2()
    {
        menuTextAnimator.SetTrigger("hightlight2");
    }
    public void OnPress2()
    {
        menuTextAnimator.SetTrigger("press2");
    }
    public void OnSelect2()
    {
        menuTextAnimator.SetTrigger("select2");
    }

    public void OnDisabled2()
    {
        menuTextAnimator.SetTrigger("disable2");
    }

}
