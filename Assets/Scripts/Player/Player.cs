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
    //����һ�������ֵ����ڲ���״̬����Ҫ����PlayerState�е�ö��״̬����
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

    //���ֵ��Add�������һ��Idle״̬����
    //Add(��ֵ������)
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
    //ͨ����ȡ����� ˮƽ��ת��������Ķ�ά����ת��Ϊ ��������ӽǵ���ά�ƶ�����ȷ����ɫ�ƶ�ʼ������ҿ����Ļ��淽��һ�¡����Ĳ���Ϊ��
    //��ȡ�����ˮƽ��ת��
    //������ת���ǰ������ҷ���
    //����������Ϸ���
    //ˮƽ������һ������������

    //�����ת��Ӧ��ŷ���Ǳ仯������Ϊ��x��z��y��
    // �����߼���ͨ��ˮƽ��ת�Ƕȵ������뷽��ʹ��ҵ��ƶ�����ʼ��������������
    //�˴�inputΪ���̵�move���루WASD��
    public Vector3 GetRelativeDirection(Vector3 input)
    {
        //��ȡ�������������ϵy�����ת�Ƕȣ�����һ����Ԫ���洢������y�����ת
        //������Ŀ���ǽ�������������ƶ������ĽǶ���ת
        Quaternion rot = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

        //��������ƶ����򣬿�����ô�����ʵ���������WS�����Ӧ������+AD�����Ӧ��������ֻ������ͨ��rot���ֱ��ǰ�������������н���
        //�������ֱ�Ϊ rot*Vector3.forward �� rot*Vector3.right
        //���������϶��ɵľ���һ����ά������dir
        Vector3 dir = rot * Vector3.forward * input.y + rot * Vector3.right * input.x;

        //ˮƽ������һ��������������һ��ȷ���ƶ��ٶȺ㶨������б���ƶ�����
        return new Vector3(dir.x, 0, dir.z).normalized;
    }


    public void CharacterRotationControl(Vector3 input)
    {
        //���㵱ǰ����������x��֮��ļн�
        //Atan2�᷵��tanΪy/x��ֵ����Ӧ��������x,y����x��֮��ļнǣ��Ի���Ϊ��λ

        data.rotationAngle =
        Mathf.Atan2(input.x, input.y) *
        Mathf.Rad2Deg + mainCamera.eulerAngles.y;


        
        //����ת��仯
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, data.rotationAngle,
        ref data.angleVelocity, data.rotationSmoothTime);


    }

    #region ��������
    public void AnimationBegin()
    {
        isAnimationFinished = false ;
    }

    public void AnimationFinish()
    {
        isAnimationFinished = true ;
    }

    #endregion


    #region ��ʵ�ַ���
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
