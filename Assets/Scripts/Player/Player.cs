using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

[RequireComponent(typeof(Animator), typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public PlayerData data;
    public GameManager game;
    public CharacterController controller;
    public Animator anim;
    public PlayerInput input;

    public bool isAnimationFinished;

    private Transform mainCamera;


    public IPlayerState current;
    public IPlayerState last;
    //声明一个泛型字典用于查找状态，需要传入PlayerState中的枚举状态种类
    public Dictionary<PlayerState.PlayerStateType, IPlayerState> states = new Dictionary<PlayerState.PlayerStateType, IPlayerState>();


    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main.transform;
        input = new PlayerInput();
        input.Enable();
        game = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        anim = this?.GetComponent<Animator>();
        controller = this?.GetComponent<CharacterController>();
        isAnimationFinished = false ;

    //用字典的Add方法添加一个Idle状态类型
    //Add(键值，对象)
        states.Add(PlayerStateType.Idle, new Idle(this));
        states.Add(PlayerStateType.Move, new Move(this));
        states.Add(PlayerStateType.Attack, new Attack(this));
        states.Add(PlayerStateType.TurnBack, new TurnBack(this));
        TransState(PlayerStateType.Idle);
    }

    void Start()
    {
        
    }


    void Update()
    {
        current.OnUpdate();
    }

    public void TransState(PlayerStateType type)
    {
        if (current != null)
        {
            current.OnExit();
            last = current;
        }

        current = states[type];
        current.OnEnter();
    }
    //通过获取相机的 水平旋转，将输入的二维方向转换为 基于相机视角的三维移动方向，确保角色移动始终与玩家看到的画面方向一致。核心步骤为：
    //提取相机的水平旋转。
    //计算旋转后的前方向和右方向。
    //根据输入组合方向。
    //水平化并归一化方向向量。

    //相机旋转是应用欧拉角变化，坐标为（x，z，y）
    // 核心逻辑是通过水平旋转角度调整输入方向，使玩家的移动方向始终与相机方向对齐
    //此处input为键盘的move输入（WASD）
    public Vector3 GetRelativeDirection(Vector3 input)
    {
        //获取相机绕世界坐标系y轴的旋转角度，并用一个四元数存储仅包含y轴的旋转
        //这样的目的是仅考虑鼠标左右移动带来的角度旋转
        Quaternion rot = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

        //计算相对移动方向，可以这么理解其实就是在组合WS输入对应的向量+AD输入对应的向量，只不过是通过rot来分别对前后和左右输入进行矫正
        //矫正量分别为 rot*Vector3.forward 和 rot*Vector3.right
        //这样最后组合而成的就是一个三维向量，dir
        Vector3 dir = rot * Vector3.forward * input.y + rot * Vector3.right * input.x;

        //水平化并归一化方向向量，归一化确保移动速度恒定，避免斜向移动更快
        return new Vector3(dir.x, 0, dir.z).normalized;
    }


    public void CharacterRotationControl(Vector3 input)
    {
        //计算当前输入向量与x轴之间的夹角
        //Atan2会返回tan为y/x的值所对应的向量（x,y）与x轴之间的夹角，以弧度为单位

        data.rotationAngle =
        Mathf.Atan2(input.x, input.y) *
        Mathf.Rad2Deg + mainCamera.eulerAngles.y;


        
        //进行转向变化
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, data.rotationAngle,
        ref data.angleVelocity, data.rotationSmoothTime);


    }

    #region 动画方法
    public void AnimationBegin()
    {
        isAnimationFinished = false ;
    }

    public void AnimationFinish()
    {
        isAnimationFinished = true ;
    }

    #endregion


    #region 待实现方法
    public void EnablePreInput()
    {

    }

    public void PlayVFX()
    {

    }

    public void ATK()
    {

    }

    public void CancelAttackColdTime()
    {

    }

    public void PlayWeaponEndSound()
    {

    }

    public void PlayWeaponBackSound()
    {

    }

    public void DisableLinkCombo()
    {

    }

    #endregion

}
