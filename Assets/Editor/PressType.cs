using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
[CustomEditor(typeof(PressBlock))]
public class PressType : Editor
{
    public override void OnInspectorGUI()
    {
        PressBlock obj = target as PressBlock;



        EditorGUI.BeginChangeCheck();
        obj.CinemachineCollisionImpulseSource = EditorGUILayout.ObjectField("ChinemachineImpuluse", obj.CinemachineCollisionImpulseSource, typeof(CinemachineCollisionImpulseSource), true) as CinemachineCollisionImpulseSource;
        obj.startPos = EditorGUILayout.Vector2Field("開始位置",obj.startPos);
        obj.backSpeed = EditorGUILayout.FloatField("上昇速度", obj.backSpeed);
        obj.pressPower = EditorGUILayout.FloatField("落下力", obj.pressPower);
        obj.cooltime = EditorGUILayout.FloatField("クールタイム", obj.cooltime);
        obj.rb = EditorGUILayout.ObjectField("Rigidbody",obj.rb, typeof(Rigidbody2D), true) as Rigidbody2D;
        obj.isStart = EditorGUILayout.Toggle("isStart", obj.isStart);
        obj.isCoolDown = EditorGUILayout.Toggle("isCoolDown", obj.isCoolDown);
        obj.pressTimeType = (PressBlock.PressTimeType)EditorGUILayout.EnumPopup("落下時間タイプ", obj.pressTimeType);
        obj.aud_SE = EditorGUILayout.ObjectField("AudioSource",obj.aud_SE,typeof(AudioSource),true) as AudioSource;
        switch (obj.pressTimeType)
        {
            case PressBlock.PressTimeType.Random:
                obj.startRandomCoolTimeRangeMin = EditorGUILayout.IntField("最低時間",obj.startRandomCoolTimeRangeMin);
                obj.startRandomCoolTimeRangeMax = EditorGUILayout.IntField("最高時間", obj.startRandomCoolTimeRangeMax);
                break;

            case PressBlock.PressTimeType.cons:
                obj.pressTime = EditorGUILayout.IntField("落下時間",obj.pressTime);

                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(obj);
        }
    }
}
