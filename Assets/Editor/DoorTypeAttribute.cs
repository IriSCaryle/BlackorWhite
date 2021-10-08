using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Door))]
public class DoorTypeAttribute : Editor
{
    SerializedProperty _list;
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();


        Door obj = target as Door;

        _list = serializedObject.FindProperty("switchObjs");
        serializedObject.Update();

        

        GUIStyle TitleLabelstyle = new GUIStyle()
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 15

        };
        
        EditorGUILayout.LabelField("Positions",TitleLabelstyle);
        GUILayout.Space(10);

        obj.openPos = EditorGUILayout.Vector2Field("開位置", obj.openPos);

        obj.closePos = EditorGUILayout.Vector2Field("閉位置", obj.closePos);

        EditorGUILayout.LabelField("ObjectInfo", TitleLabelstyle);
        GUILayout.Space(10);

        obj.doorObj = EditorGUILayout.ObjectField("ドア", obj.doorObj, typeof(GameObject), true) as GameObject;

        obj.speed = EditorGUILayout.FloatField("開閉速度", obj.speed);

        EditorGUILayout.LabelField("Debug", TitleLabelstyle);
        GUILayout.Space(10);
        obj.isOpen = EditorGUILayout.Toggle("isOpen", obj.isOpen);
        EditorGUILayout.LabelField("Options", TitleLabelstyle);
        GUILayout.Space(10);

        obj.doorType = (Door.DoorType)EditorGUILayout.EnumPopup("開閉タイプ", obj.doorType);
        EditorGUI.indentLevel++;

        switch (obj.doorType)
        {
            case Door.DoorType.AutomaticDooor:
                obj.automaticDoor_param.closeTime = EditorGUILayout.FloatField("閉まる時間", obj.automaticDoor_param.closeTime);
                break;
            case Door.DoorType.SwitchDoor:
                obj.switchDoor_param.changeNum = EditorGUILayout.IntField("スイッチの数", obj.switchDoor_param.changeNum);
                obj.switchDoor_param.switchObj = EditorGUILayout.ObjectField("スイッチ1", obj.switchDoor_param.switchObj, typeof(Switch), true) as Switch;
                obj.switchDoor_param.switchObj2 = EditorGUILayout.ObjectField("スイッチ2", obj.switchDoor_param.switchObj2, typeof(Switch), true) as Switch;
                obj.isInvert = EditorGUILayout.Toggle("反転スイッチ", obj.isInvert);
                if (obj.isInvert)
                {
                    obj.invertSwitchDoor_param.changeNum = EditorGUILayout.IntField("スイッチの数", obj.invertSwitchDoor_param.changeNum);
                    obj.invertSwitchDoor_param.switchObj = EditorGUILayout.ObjectField("スイッチ1", obj.invertSwitchDoor_param.switchObj, typeof(InvertSwitch), true) as InvertSwitch;
                    obj.invertSwitchDoor_param.switchObj2 = EditorGUILayout.ObjectField("スイッチ2", obj.invertSwitchDoor_param.switchObj2, typeof(InvertSwitch), true) as InvertSwitch;
                }
                
                break;
            case Door.DoorType.TimeDoor:
                obj.timeDoor_param.cooltime = EditorGUILayout.FloatField("閉まる時間", obj.timeDoor_param.cooltime);
                obj.timeDoor_param.time = EditorGUILayout.FloatField("開く時間", obj.timeDoor_param.time);
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(obj);
        }
    }
}
    