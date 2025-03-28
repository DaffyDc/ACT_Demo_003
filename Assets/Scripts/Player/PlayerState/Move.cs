using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region �ƶ�״̬

public class Move : State
{
    //�ù��캯��Լ������Player����
    public Move(Player self) : base(self)
    {
        this.self = self;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        self.anim.Play("Run_Start");
        self.anim.SetBool("Run", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //��magnitude����������ƽ�����������ĳ��ȣ�ͨ�������ĳ��ȴ���һ����Сֵ�����ж��Ƿ��������



        if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude <= 0.05f)
        {

            self.anim.SetBool("Run", false);
            self.TransState(PlayerStateType.Idle);
            Debug.Log("�л�������״̬");

        }

        else if (self.input.PlayerBasic.Attack.IsPressed())
        {
            self.TransState(PlayerStateType.Attack);
            Debug.Log("�л�������״̬");
        }
        //Debug.Log(Vector3.Angle(self.transform.forward, Vector3.one * self.input.PlayerBasic.Move.ReadValue<Vector2>()));


        else
        {
            //һ���ƶ��ķ���   self.data.speed * 
            //self.controller.Move(Time.deltaTime * self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>()));
            //self.transform.LookAt(self.transform.position + self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>()));
            //��Ϊ�����Դ��ƶ�����������ֻ��Ҫȷ������ת�򼴿�
            self.CharacterRotationControl(self.input.PlayerBasic.Move.ReadValue<Vector2>());
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }


}
#endregion
