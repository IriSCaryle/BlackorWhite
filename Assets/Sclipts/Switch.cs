using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public Vector2 leverOnPos;

    public Vector2 leverOffPos;

    public bool isOn;

    public GameObject lever;

    public float speed;

    bool isRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnOffSwitch();
        CheckOnTrigger();
    }

    void OnOffSwitch()
    {
        if (isOn)
        {
            lever.transform.localPosition = Vector2.MoveTowards(lever.transform.localPosition, leverOnPos, speed * Time.deltaTime);
        }
        if (!isOn)
        {
            lever.transform.localPosition = Vector2.MoveTowards(lever.transform.localPosition, leverOffPos, speed * Time.deltaTime);
        }
    }

    void CheckOnTrigger()
    {
        if (isRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOn = !isOn;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isRange = false;
        }
    }

}
