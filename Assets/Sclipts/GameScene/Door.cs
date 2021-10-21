using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ドアというか扉のスクリプト
/// </summary>
public class Door : MonoBehaviour
{  
    public Vector2 closePos;//閉じているときの位置
    public Vector2 openPos;//開けているときの位置
    public GameObject doorObj;//prefab
    public float speed;//開閉速度

    public DoorType doorType;//ドアの開閉条件
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

    public bool isOpen;//空いているか

    public bool isInvert;//
    void OnDrawGizmos()
    {
        switch (doorType)
        {
            case DoorType.AutomaticDooor://自動ドア
            
                break;
            case DoorType.SwitchDoor://スイッチでドアが開きます

                break;
            case DoorType.TimeDoor://時間でドアが開きます

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


    void OpenClose()//ドアの開閉動作
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

    void CheckSwitches()//スイッチを監視します
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

    bool CheckSwitch()//指定したスイッチが切り替わっているか判定します
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


   bool CheckInvertSwitch()//スイッチの方向が反転したスイッチを反転します
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
public class AutomaticDoor//自動ドアクラス
{
    public float closeTime;
}
[System.Serializable]
public class SwitchDoor//スイッチドアクラス
{
    public int changeNum;
    public Switch switchObj;
    public Switch switchObj2;
    
}

[System.Serializable]
public class InvertSwitchDoor//反転スイッチドアクラス
{
    public int changeNum;
    public InvertSwitch switchObj;
    public InvertSwitch switchObj2;
}

[System.Serializable]
public class TimeDoor//時間ドアクラス
{
    public float time;

    public float cooltime;


}

