using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeColider : MonoBehaviour
{
    [SerializeField] SlimeEnemy slimeEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            slimeEnemy.player = collision.gameObject;
            slimeEnemy.chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            slimeEnemy.player = null;
            slimeEnemy.chase = false;
        }
    }
}
