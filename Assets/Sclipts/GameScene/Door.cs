using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 closePos;
    public Vector2 openPos;

    public GameObject doorObj;

    public float speed;


    public DoorType doorType;
    public enum DoorType
    {
        AutomaticDooor,
        SwitchDoor,
        TimeDoor,

    }
    public AutomaticDoor automaticDoor_param;
    public SwitchDoor switchDoor_param;
    public TimeDoor timeDoor_param;

    public InvertSwitchDoor invertSwitchDoor_param;

    public bool isOpen;

    public bool isInvert;
    void OnDrawGizmos()
    {
        switch (doorType)
        {
            case DoorType.AutomaticDooor:
            
                break;
            case DoorType.SwitchDoor:

                break;
            case DoorType.TimeDoor:

                break;
        }

    }
        // Start is called before the first frame update
    void Start()
    {
         isOpen = false;

   
    }

    // Update is called once per frame
    void Update()
    {
        OpenClose();

        switch (doorType)
        {
            case DoorType.AutomaticDooor:

                break;
            case DoorType.SwitchDoor:
                CheckSwitches();
                break;
            case DoorType.TimeDoor:

                break;
        }
    }


    void OpenClose()
    {
        if (isOpen)
        {
            doorObj.transform.localPosition = Vector2.MoveTowards(doorObj.transform.localPosition, openPos, speed * Time.deltaTime);
        }
        if (!isOpen)
        {
            doorObj.transform.localPosition = Vector2.MoveTowards(doorObj.transform.localPosition, closePos, speed * Time.deltaTime);
        }
    }

    void CheckSwitches()
    {
        if (!isInvert)
        {
            if (CheckSwitch())
            {
                isOpen = true;
            }
            else
            {
                isOpen = false;
            }
        }
        else
        {
            if (CheckSwitch() && CheckInvertSwitch()){

                isOpen = true;
            }
            else
            {
                isOpen = false;
            }
        }
    }

    bool CheckSwitch()
    {
        switch (switchDoor_param.changeNum)
        {
            case 0:
                Debug.LogError("スイッチの数が0です");
                break;
            case 1:
                if (switchDoor_param.switchObj.isOn)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            case 2:
                if (switchDoor_param.switchObj.isOn && switchDoor_param.switchObj2.isOn)
                {
                    return true;
                }
                else
                {
                    return false;
                }  
            default:
                return false;
                
        }

        return false;
    }


   bool CheckInvertSwitch()
    {
        switch (invertSwitchDoor_param.changeNum)
        {
            case 0:
                Debug.LogError("スイッチの数が0です");
                break;
            case 1:
                if (invertSwitchDoor_param.switchObj.isOn)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (invertSwitchDoor_param.switchObj.isOn && invertSwitchDoor_param.switchObj2.isOn)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
                return false;
        }

        return false;
    }

    
}
[System.Serializable]
public class AutomaticDoor
{
    public float closeTime;
}
[System.Serializable]
public class SwitchDoor
{
    public int changeNum;
    public Switch switchObj;
    public Switch switchObj2;
    
}

[System.Serializable]
public class InvertSwitchDoor
{
    public int changeNum;
    public InvertSwitch switchObj;
    public InvertSwitch switchObj2;
}

[System.Serializable]
public class TimeDoor
{
    public float time;

    public float cooltime;


}

