using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    //��һ��ƫ�����ķ�Χ����
    //layer�ϰ���Ĳ㼶
    [SerializeField, Header("��ײƫ����")] private Vector2 _maxDistanceOffset;
    [SerializeField, Header("���㼶"), Space(height: 10)] private LayerMask _whatIsWall;
    [SerializeField, Header("���߳���"), Space(height: 10)] private float _detectionDistance;
    [SerializeField, Header("��ײ�ƶ�ƽ��ʱ��"), Space(height: 10)] private float _colliderSmoothTime;

    //��ʼ��ʱ�򣬱���һ����ʼ�����ʼ��ƫ����
    private Vector3 _originPosition;
    private float _originOffsetDistance;
    private Transform _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main.transform;
    }
    void Start()
    {
        _originPosition = transform.localPosition.normalized;//ȡ��ʼ��ƫ�����ı�׼ֵ
        _originOffsetDistance = _maxDistanceOffset.y;

    }

    private void LateUpdate()
    {
        UpdateCameraCollider();
    }

    private void UpdateCameraCollider()
    {
        //ת������������ϵ��ȷ�����ĵ���ת���䣬����������ƶ��Ĺ����л�����ײ����߷����һΡ�
        var detectionDirection = transform.TransformPoint(_originPosition * _detectionDistance);
        if (Physics.Linecast(transform.position, detectionDirection, out var hit, _whatIsWall, QueryTriggerInteraction.Ignore))
        {
            //����򵽶����ˣ�˵��������������Ҫ�����ǰ�ƶ�
            //ʹ��clamp�����������ߵķ�Χ����hit�ľ����ڷ����ڸ�ֵ
            //out�����������õ������ڷ����ڸ�ֵ
            //�˴���0.8��Ϊ�˷�ֹ�߶μ�⵽ǽ��ʱ��������Ѿ������˴�͸
            _originOffsetDistance = Mathf.Clamp((hit.distance * 0.8f), _maxDistanceOffset.x, _maxDistanceOffset.y);
        }
        else
        {
            _originOffsetDistance = _maxDistanceOffset.y;
        }
        _mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition, _originPosition * (_originOffsetDistance - 0.1f),
            UnTetheredLerp(_colliderSmoothTime));
    }


    //һ������֡��Ӱ���Lerp����
    public static float UnTetheredLerp(float time = 10f)
    {
        return 1 - Mathf.Exp(-time * Time.deltaTime);
    }
}
