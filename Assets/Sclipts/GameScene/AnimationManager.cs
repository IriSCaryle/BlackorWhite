using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// メッセージボードのテキストの点滅アニメーションの管理
/// </summary>
public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void BlickStart()
    {
        animator.SetTrigger("isopen");
    }

    public void BlinkStop()
    {
        animator.SetTrigger("isstop");
    }
}
