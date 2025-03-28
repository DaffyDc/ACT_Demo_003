using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ����״̬
public class Idle : State
{

    public Idle(Player self) : base(self)
    {
        this.self = self;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //self.anim.Play("Idle");

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Vector3.Angle(self.transform.forward, Vector3.one * self.input.PlayerBasic.Move.ReadValue<Vector2>()) > 90)
        {
            Debug.Log(Vector3.Angle(self.transform.forward, Vector3.one * self.input.PlayerBasic.Move.ReadValue<Vector2>()));
            //�군��ת���ܹ������ǻ��ڵ���ת����취����һ��,�а취�˵�����ת������һ��״̬���ͺ���
            self.TransState(PlayerStateType.TurnBack);
            Debug.Log("��ʼת����");
        }
        //��magnitude����������ƽ�����������ĳ��ȣ�ͨ�������ĳ��ȴ���һ����Сֵ�����ж��Ƿ��������
        else if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
        {
            self.TransState(PlayerStateType.Move);
            Debug.Log("�л����ƶ�״̬");
        }

        else if (self.input.PlayerBasic.Attack.WasPerformedThisFrame())
        {
            self.TransState(PlayerStateType.Attack);
            Debug.Log("�л�������״̬");
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }


}
#endregion
