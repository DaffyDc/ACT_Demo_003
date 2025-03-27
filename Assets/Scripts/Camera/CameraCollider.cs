using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    //有一个偏移量的范围区间
    //layer障碍物的层级
    [SerializeField, Header("碰撞偏移量")] private Vector2 _maxDistanceOffset;
    [SerializeField, Header("检测层级"), Space(height: 10)] private LayerMask _whatIsWall;
    [SerializeField, Header("射线长度"), Space(height: 10)] private float _detectionDistance;
    [SerializeField, Header("碰撞移动平滑时间"), Space(height: 10)] private float _colliderSmoothTime;

    //开始的时候，保存一下起始点和起始的偏移量
    private Vector3 _originPosition;
    private float _originOffsetDistance;
    private Transform _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main.transform;
    }
    void Start()
    {
        _originPosition = transform.localPosition.normalized;//取初始化偏移量的标准值
        _originOffsetDistance = _maxDistanceOffset.y;

    }

    private void LateUpdate()
    {
        UpdateCameraCollider();
    }

    private void UpdateCameraCollider()
    {
        //转换到世界坐标系，确保轴心点旋转不变，否则相机在移动的过程中会让碰撞检测线方向乱晃。
        var detectionDirection = transform.TransformPoint(_originPosition * _detectionDistance);
        if (Physics.Linecast(transform.position, detectionDirection, out var hit, _whatIsWall, QueryTriggerInteraction.Ignore))
        {
            //如果打到东西了，说明碰到东西，需要相机往前移动
            //使用clamp方法限制射线的范围，给hit的距离在方法内赋值
            //out参数传递引用但必须在方法内赋值
            //此处乘0.8是为了防止线段检测到墙壁时，相机就已经发生了穿透
            _originOffsetDistance = Mathf.Clamp((hit.distance * 0.8f), _maxDistanceOffset.x, _maxDistanceOffset.y);
        }
        else
        {
            _originOffsetDistance = _maxDistanceOffset.y;
        }
        _mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition, _originPosition * (_originOffsetDistance - 0.1f),
            UnTetheredLerp(_colliderSmoothTime));
    }


    //一个不受帧数影响的Lerp方法
    public static float UnTetheredLerp(float time = 10f)
    {
        return 1 - Mathf.Exp(-time * Time.deltaTime);
    }
}
