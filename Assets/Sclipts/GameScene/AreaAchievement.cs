using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAchievement : MonoBehaviour
{
    [Header("実績ID")]
    [SerializeField] int ID;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.CheckAchievement(ID);
        }
    }
}
