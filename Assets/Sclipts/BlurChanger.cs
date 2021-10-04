using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurChanger : MonoBehaviour
{
    [SerializeField] SuperBlur.SuperBlur superBlur;
    [SerializeField] float MaxInterpolation = 0.698f;
    [SerializeField] float speed;
    [SerializeField,Range(0,0.699f)] float _interpolation; 
    bool isBlur;
    bool isNormal;
    // Start is called before the first frame update
    void Start()
    {
        isBlur = false;
        isNormal = false ;
        _interpolation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlur)
        {
            _interpolation +=speed * Time.deltaTime;
            superBlur.interpolation = _interpolation;
            if (superBlur.interpolation >= MaxInterpolation)
            {
                superBlur.interpolation = 0.698f;
                isBlur = false;
  
            }
        }


        if (isNormal)
        {
            _interpolation -= speed * Time.deltaTime;
            superBlur.interpolation = _interpolation;
            if (superBlur.interpolation <= 0)
            {
                superBlur.interpolation = 0;
                isNormal = false;

            }
        }
    }

    public void OnBlur()
    {
        isBlur = true;
    }
    public void OnNormal()
    {
        isNormal = true;
    }
}
