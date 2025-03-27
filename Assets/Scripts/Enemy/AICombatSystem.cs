using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICombatSystem : MonoBehaviour
{
    [SerializeField, Header("��Χ���")] private Transform detectionCenter;
    [SerializeField] private float detectionRange;

    [SerializeField] private int targetCount; 

    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsObs;

    public Collider[] colliderTarget = new Collider[1];

    [SerializeField, Header("Ŀ��")] private Transform currentTarget;


    private void AIView()
    {
        targetCount =
            Physics.OverlapSphereNonAlloc(detectionCenter.position, detectionRange, colliderTarget, whatIsEnemy);
        if (targetCount > 0)
        {
            if (Physics.Raycast((transform.root.position + transform.up * 0.5f), (colliderTarget[0].transform.position - transform.root.position).normalized, out var hit, detectionRange, whatIsObs))
            {
                //Dot���������������ĵ��������1˵��������ͬ������-1�����෴������0����ֱ
                if(Vector3.Dot((colliderTarget[0].transform.position - transform.root.position).normalized, transform.root.forward) > 0.15f)
                {
                    currentTarget = colliderTarget[0].transform;
                }
            }
        }
    }

}
