using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpUI : MonoBehaviour
{
    [SerializeField] Text text1;
    [SerializeField] Text text2;
    [SerializeField] float speed;
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
