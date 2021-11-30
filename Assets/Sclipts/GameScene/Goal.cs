using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ゴールオブジェクト
/// </summary>
public class Goal : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource aud_SE;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)//コライダーがプレイヤーを検出するとGameManagerに通知します
    {
        if(collision.gameObject.tag == "Player")
        {
            gameManager.StageGoal();
        }
    }

}
