using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Invertスクリプトのプレイヤー版(リグごとにSpriteRendererが分かれてるため)
/// </summary>
public class InvertPlayer : MonoBehaviour
{
    [Header("カメラ名")]
    [SerializeField] string cameraName;
    [Header("オブジェクトがカメラに写っているか")]
    public bool isRender = false;
    [Header("Sprites")]
    [SerializeField] List<SpriteRenderer> rigs;
    [Header("オブジェクトをワールドの色と同化するようにするか")]
    [SerializeField] bool isTransparent;
    [SerializeField, Range(0f, 1f)] float colorValue;
    [SerializeField] float changeSpeed;
    [SerializeField] PleyerSclipt playerSclipt;

    public DefalutColor defaultColor;


    bool isBlack = false;
    bool isWhite = false;


    public enum DefalutColor
    {
        Black = 0,
        White = 1,
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isTransparent)
        {
            if (playerSclipt.avility)
            {
              
                ChangeInvertBool();
            }
            ChangeColorRender();
            isRender = false;
        }
    }



   

    void ChangeInvertBool()
    {

        if (rigs[0].color == Color.black)
        {
            isWhite = true;
        }
        else if (rigs[0].color == Color.white)
        {
            isBlack = true;
        }

    }

    void ChangeColorRender()
    {
        if (isBlack)
        {
            colorValue += Time.deltaTime * changeSpeed;
            Color color = Color.Lerp(Color.white, Color.black, colorValue);
            for(int i = 0; i < rigs.Count; i++)
            {
                rigs[i].color = color;
            }
            if (colorValue >= 1)
            {
                colorValue = 0;
                isBlack = false;
            }
        }

        if (isWhite)
        {
            colorValue += Time.deltaTime * changeSpeed;
            Color color = Color.Lerp(Color.black, Color.white, colorValue);
            for (int i = 0; i < rigs.Count; i++)
            {
                rigs[i].color = color;
            }
            if (colorValue >= 1)
            {
                colorValue = 0;
                isWhite = false;
            }

        }
    }

    

   

}
