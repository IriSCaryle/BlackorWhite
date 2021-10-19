using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Invert : MonoBehaviour
{
    [Header("カメラ名")]
    [SerializeField] string cameraName;
    [Header("オブジェクトがカメラに写っているか")]
    public bool isRender = false;
    [Header("Sprite")]
    [SerializeField] SpriteRenderer sprite;
    [Header("オブジェクトをワールドの色と同化するようにするか")]
    [SerializeField] bool isTransparent;
    [Header("色変化値")] 
    [SerializeField, Range(0f, 1f)] float colorValue;
    [Header("色遷移速度")]
    [SerializeField] float changeSpeed;
    [Header("プレイヤースクリプト")]
    [SerializeField] PleyerSclipt playerSclipt;
    [Header("コライダー")]
    [SerializeField] BoxCollider2D collider2D;
    [Header("オブジェクトの初期色")]
    public DefalutColor defaultColor;
    [Header("オブジェクトのisTriggerの初期値")]
    [SerializeField] bool defaultIsTrigger;


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
        if (GetComponent<BoxCollider2D>())
        {
            collider2D = GetComponent<BoxCollider2D>();
        }
        if (playerSclipt == null)
        {
            playerSclipt = GameObject.FindGameObjectWithTag("Player").GetComponent<PleyerSclipt>();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isTransparent)
        {
            if (playerSclipt.avility)
            {
                isRendered();

            }
            ChangeColorRender();
            isRender = false;
        }
        else if(isTransparent)
        {
            if (playerSclipt.avility)
            {
                CheckWorldColor();
            }
        }
    }

    void CheckWorldColor()
    {
        if ((int)defaultColor == (int)playerSclipt.worldType)
        {
            Debug.Log("Invert:色が同化したので判定を削除します-"+gameObject.name);
            if (collider2D != null && defaultIsTrigger == true)
            {
                collider2D.isTrigger = true;
            }
            else if(collider2D != null && defaultIsTrigger == false)
            {
                collider2D.isTrigger = true;
            }
        }
        else
        {
            if (collider2D != null && defaultIsTrigger == true)
            {
                collider2D.isTrigger = true;
            }
            else if (collider2D != null && defaultIsTrigger == false)
            {
                collider2D.isTrigger = false;
            }
        }
    }

    void isRendered()
    {
        if (isRender)
        {
            Debug.Log("invertCam:映っている");
            ChangeInvertBool();
        }
        if (!isRender)
        {
            Debug.Log("invertCam:映っていない");
            ChangeColorNotRender();
        }
    }

    void ChangeInvertBool()
    {

        if (sprite.color == Color.black)
        {
            isWhite = true;
        }else if (sprite.color == Color.white)
        {
            isBlack = true; 
        }
        
    }

    void ChangeColorRender()
    {
        if (isBlack)
        {
            colorValue += Time.deltaTime*changeSpeed;
            Color color = Color.Lerp(Color.white, Color.black, colorValue);
            sprite.color = color;
            if (colorValue >= 1)
            {
                colorValue = 0;
                isBlack = false;              
            }
        }

        if (isWhite)
        {
            colorValue += Time.deltaTime*changeSpeed;
            Color color = Color.Lerp(Color.black, Color.white, colorValue);
            sprite.color = color;
            if (colorValue >= 1)
            {
                colorValue = 0;
                isWhite = false;           
            }

        }
    }

    void ChangeColorNotRender()
    {

        if (sprite.color == Color.white)
        {
            Debug.Log("invert:黒");
            sprite.color = Color.black;
        }
        else if (sprite.color == Color.black)
        {
            Debug.Log("invert:白");
            sprite.color = Color.white;
        }
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.tag == cameraName)
        {
            isRender = true;
        }
    }
        
}
