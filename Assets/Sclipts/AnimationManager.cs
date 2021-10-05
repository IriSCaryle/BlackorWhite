using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
