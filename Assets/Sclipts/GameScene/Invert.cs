using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 反転動作のスクリプト
/// </summary>
public class Invert : MonoBehaviour
{
    [Header("カメラ名")]
    [SerializeField] string cameraName;
    [Header("オブジェクトがカメラに写っているか")]
    public bool isRender = false;
    /// 
    /// サブカメラはメインカメラよりも範囲が広く設定されメインカメラに映る前にあらかじめ
    /// このオブジェクトがサブカメラに映っているか判定を取るために使いました
    /// 
    [Header("Sprite")]
    public SpriteRenderer sprite;
    [Header("オブジェクトをワールドの色と同化するようにするか")]
    [SerializeField] bool isTransparent;
    [Header("色変化値")] 
    [SerializeField, Range(0f, 1f)] float colorValue;
    [Header("色遷移速度")]
    [SerializeField] float changeSpeed;
    [Header("プレイヤースクリプト")]
    public PleyerSclipt playerSclipt;
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
        if (GetComponent<BoxCollider2D>())//ボックスコライダーがあれば取得
        {
            collider2D = GetComponent<BoxCollider2D>();
        }
        if (playerSclipt == null)//プレイヤースクリプトが無ければタグから取得
        {
            playerSclipt = GameObject.FindGameObjectWithTag("Player").GetComponent<PleyerSclipt>();
        }

        

        CheckWorldColor();
    }

    private void OnEnable()
    {
        if (gameObject.tag == "Bullet")
        {
            Debug.Log("Bullet:黒白検出");
           
            BulletCheckColor();
        }
    }

   


    public void BulletCheckColor()
    {
        if((int)defaultColor == (int)playerSclipt.worldType)
        {
            isRendered();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isTransparent)
        {
            if (playerSclipt.avility)//Qが押された判定
            {
                isRendered();

            }
            ChangeColorRender();
            isRender = false;
        }
        if(isTransparent)
        {
            if (playerSclipt.avility)//Qが押された判定
            {
                CheckWorldColor();
            }
        }
    }

    void CheckWorldColor()//(*)世界の色をチェックし背景と同化した場合はコライダーをオフにします   /* (*)世界の色:プレイヤースクリプト参照 */
    {
        if (gameObject.tag != "Bullet")
        {
            if ((int)defaultColor == (int)playerSclipt.worldType)//同化した場合
            {
                Debug.Log("Invert:色が同化したので判定を削除します-" + gameObject.name);
                if (collider2D != null && defaultIsTrigger == true)
                {
                    collider2D.enabled = false;
                }
                else if (collider2D != null && defaultIsTrigger == false)
                {
                    collider2D.isTrigger = true;
                }
            }
            else//していない場合
            {
                if (collider2D != null && defaultIsTrigger == true)
                {
                    collider2D.enabled = true;
                }
                else if (collider2D != null && defaultIsTrigger == false)
                {
                    collider2D.isTrigger = false;
                }
            }
        }
    }

    void isRendered()//サブカメラに写っているか
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

    void ChangeInvertBool()//カメラに映っている際,黒と白の色を遷移させる動作切り替え
    {

        if (sprite.color == Color.black )
        {
            isWhite = true;
        }else if (sprite.color == Color.white)
        {
            isBlack = true; 
        }
        
    }

    void ChangeColorRender()//色遷移動作
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

    void ChangeColorNotRender()//映っていない場合の動作です,映っていない場合は軽量化のためすぐ切り替わるようになっています
    {

        if (sprite.color == Color.white)
        {
            Debug.Log("invert:黒");
            sprite.color = Color.black;
        }
        else if (sprite.color == Color.black )
        {
            Debug.Log("invert:白");
            sprite.color = Color.white;
        }
    }

    private void OnWillRenderObject()//カメラのタグが合っていればカメラに映った際isRendeeがtrueを返します
    {
        if (Camera.current.tag == cameraName)
        {
            isRender = true;
        }
    }
        
}
