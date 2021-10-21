using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// オブジェクトの上に表示するテキストの動作
/// </summary>
public class PopUpUI : MonoBehaviour
{
    [Header("表示するテキスト")]
    [SerializeField] Text text1;
    [SerializeField] Text text2;
    [Header("フェードスピード")]
    [SerializeField] float speed;
    [Header("フェード状況")]
    public bool isFadeIn;

    public bool isFadeOut;
    // Start is called before the first frame update
    void Start()
    {
        Color color = text1.color;
        color.a = 0;
        text1.color = color;
        //text2.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        //フェードイン&フェードアウト動作
        if (isFadeIn && isFadeOut)
        {
            isFadeOut = true;
            isFadeIn = false;
        }
        if (isFadeIn)
        {
            Color color = text1.color;
            color.a = color.a <= 1 ? color.a + speed :1 ;
            text1.color = color;
            //text2.color = color;
            if (color.a >= 1)
            {
                isFadeIn = false;
            }
        }
        if (isFadeOut)
        {
            Color color = text1.color;
            color.a = color.a <= 0 ? 0 : color.a - speed;
            text1.color = color;
            //text2.color = color;
            if(color.a <= 0)
            {
                isFadeOut = false;
            }
        }
    }
}
