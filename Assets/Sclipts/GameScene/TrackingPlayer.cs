using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPlayer : MonoBehaviour
{

    bool isTracking;
    [SerializeField] GameObject target;
    // Start isled before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            transform.LookAt(target.transform);
        }
    }

    public void Tracking(GameObject player)
    {
        isTracking = true;
        target = player;
    }

    public void FinishTracking()
    {
        isTracking = false;
        target = null;
    }
}
