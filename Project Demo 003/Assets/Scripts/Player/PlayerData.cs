using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Demo/Data",fileName ="PlayerDataSO")]
public class PlayerData : ScriptableObject
{
    [Header("�ƶ����")]
    [SerializeField, Header("������תƽ��ʱ��")]
    public float rotationSmoothTime;
    [SerializeField, Header("������ת�Ƕ�")]
    public float rotationAngle;
    [SerializeField, Header("������ת���ٶ�")]
    public float angleVelocity;

    public float speed = 4;

    [Header("�������")]
    public float attackCounter = 4;
}
