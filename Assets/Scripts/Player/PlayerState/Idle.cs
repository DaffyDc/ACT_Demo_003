using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ����״̬
public class Idle : State
{
    private Vector3 inputDir;
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

        inputDir = new Vector3(self.input.PlayerBasic.Move.ReadValue<Vector2>().x, 0, self.input.PlayerBasic.Move.ReadValue<Vector2>().y);

        if (Vector3.Angle(self.transform.forward,inputDir) > 135)
        {
            Debug.Log(self.transform.forward);
            Debug.Log(Vector3.Angle(self.transform.forward, (Vector3.one * self.input.PlayerBasic.Move.ReadValue<Vector2>()).normalized));

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
