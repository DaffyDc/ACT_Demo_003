using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ����״̬
public class Attack : State
{

    private float attackCounter = 1;


    //�ù��캯��Լ������Player����
    public Attack(Player self) : base(self)
    {
        this.self = self;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        switch (attackCounter)
        {
            case 1:
                self.anim.Play("Attack_Normal_01");
                Debug.Log("Player��ͨ����1��");
                break;
            case 2:
                self.anim.Play("Attack_Normal_02");
                Debug.Log("Player��ͨ����2��");
                break;
            case 3:
                self.anim.Play("Attack_Normal_03");
                Debug.Log("Player��ͨ����3��");
                break;
            case 4:
                self.anim.Play("Attack_Normal_04");
                Debug.Log("Player��ͨ����4��");
                attackCounter = 0;
                break;
        }

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (self.input.PlayerBasic.Attack.WasPressedThisFrame())
        {
            attackCounter += 1;

            switch (attackCounter)
            {
                case 1:
                    self.anim.Play("Attack_Normal_01");
                    Debug.Log("Player��ͨ����1��");
                    break;
                case 2:
                    self.anim.Play("Attack_Normal_02");
                    Debug.Log("Player��ͨ����2��");
                    break;
                case 3:
                    self.anim.Play("Attack_Normal_03");
                    Debug.Log("Player��ͨ����3��");
                    break;
                case 4:
                    self.anim.Play("Attack_Normal_04");
                    Debug.Log("Player��ͨ����4��");
                    attackCounter = 0;
                    break;
            }
        }
        if (self.isAnimationFinished == true)
        {
            self.TransState(PlayerStateType.Idle);
        }






        //��magnitude����������ƽ�����������ĳ��ȣ�ͨ�������ĳ��ȴ���һ����Сֵ�����ж��Ƿ��������
        if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
        {
            self.TransState(PlayerStateType.Move);
            Debug.Log("�ƶ�״̬");
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        attackCounter = 1;
        //private float _resetTimer = self.data._resetTimer;
    }


}
#endregion

