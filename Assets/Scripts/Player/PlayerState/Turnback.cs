using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBack : State
{
    //��Ϊ���ְ�ת�򶯻�д��Idle��Move״̬��״̬�ж����������������ᱻ��ϣ����Ծ�������дһ��״̬
    //��ӲдӦ��Ҳ��д

    public TurnBack(Player self) : base(self)
    {
        this.self = self;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        self.anim.Play("Run_Start");
        self.anim.SetBool("TurnBack",true);
        self.anim.SetBool("Run", true);
        Debug.Log("��ʼת����");

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (self.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            //self.anim.SetBool("TurnBack", false);
            //��magnitude����������ƽ�����������ĳ��ȣ�ͨ�������ĳ��ȴ���һ����Сֵ�����ж��Ƿ��������
            if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
            {
                self.TransState(PlayerStateType.Move);
                Debug.Log("�л����ƶ�״̬");
            }

            else if (self.input.PlayerBasic.Attack.WasPerformedThisFrame())
            {
                self.TransState(PlayerStateType.Attack);
                Debug.Log("�л�������״̬");
            }

            else
            {
                self.TransState(PlayerStateType.Idle);
                Debug.Log("�л�������״̬");
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        //self.anim.SetBool("TurnBack", false);
    }


}
