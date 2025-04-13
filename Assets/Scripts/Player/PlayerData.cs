using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Demo/Data",fileName ="PlayerDataSO")]
public class PlayerData : ScriptableObject
{
    [Header("镜头相关")]
    [SerializeField, Header("攻击YAxis")]
    public float attackYAngle;
    [SerializeField, Header("回退YAxis")]
    public float normalYAngle;


    [Header("移动相关")]
    [SerializeField, Header("人物旋转平滑时间")]
    public float rotationSmoothTime;
    [SerializeField, Header("人物旋转角度")]
    public float rotationAngle;
    [SerializeField, Header("人物旋转角速度")]
    public float angleVelocity;

    public float speed = 4;

    [Header("攻击相关")]
    public float attackCounter = 4;
}
