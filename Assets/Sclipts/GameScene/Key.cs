using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Animator GetAnimator;
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
            ParentChange(collision.gameObject.transform);
        }
    }


    void ParentChange(Transform parent)
    {
        gameObject.transform.parent = parent.transform;

        GetAnimator.SetTrigger("start");
    }

    public void ObjectDestroy()
    {
        Destroy(gameObject);
    }
}
