using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementAnimation : MonoBehaviour
{
    [SerializeField] GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }


    public void OnActiveAchievent()
    {
        gameManager.isActiveAchievements = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
