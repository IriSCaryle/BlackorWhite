using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// uGUIをイージングさせて動かす
/// </summary>
public class SwitchEasingScript : MonoBehaviour
{
    /*---           このスクリプトはuGui専用です   
                   
              pivotの位置の数値(anchorPosition)に合わせて動きます  
              のでこのスクリプトをアタッチした後pivotを変更しないで
              ください。位置の数値が変わってしまいます
                                                                    ----*/
    bool isOpened;
    [Header("キーを押したときに表示される位置")]
    [SerializeField] Vector2 OpenPos;
    [Header("非表示の位置")]
    [SerializeField] Vector2 ClosePos;
    [Header("イージング速度")]
    [SerializeField] float easing ;
    [Header("bool")]
    [SerializeField] InvertCoolTimeManager InvertCoolTimeManager;
    RectTransform rectTransform;
    Vector2 diff;
    Vector2 v;

    [Header("無操作時間(動作タイプがCoolDownの時のみ)")]
    [SerializeField] float cooldown; //無操作時間
    float nowcooldowntime;
  
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        rectTransform = gameObject.GetComponent<RectTransform>();
        isOpened = false;
    
        nowcooldowntime = cooldown;
    }
    // Update is called once per frame
    void Update()
    {
        //タイプ
        Toggle();     
        //開いた時の動作
        switch (isOpened)
        {
            case true:
                diff = rectTransform.anchoredPosition - OpenPos;
                v = diff * easing;
                rectTransform.anchoredPosition -= v;
                if (diff.magnitude < 0.01f)
                {
                    break;
                }
                break;
            case false:
                diff = ClosePos - rectTransform.anchoredPosition;
                v = diff * easing;
                rectTransform.anchoredPosition += v;
                if (diff.magnitude < 0.01f)
                {
                    break;
                }
                break;
        }
    }

    public void Toggle()//押したら開閉する
    {
        if (InvertCoolTimeManager.isOpen)
        {
            isOpened = true;
        }
        if (!InvertCoolTimeManager.isOpen)
        {
            isOpened = false;
        }
    }
    
    
}
