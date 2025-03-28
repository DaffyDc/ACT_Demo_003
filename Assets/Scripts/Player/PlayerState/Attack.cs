using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 攻击状态
public class Attack : State
{

    private float attackCounter = 1;


    //用构造函数约定传入Player对象
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
                Debug.Log("Player普通攻击1段");
                break;
            case 2:
                self.anim.Play("Attack_Normal_02");
                Debug.Log("Player普通攻击2段");
                break;
            case 3:
                self.anim.Play("Attack_Normal_03");
                Debug.Log("Player普通攻击3段");
                break;
            case 4:
                self.anim.Play("Attack_Normal_04");
                Debug.Log("Player普通攻击4段");
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
                    Debug.Log("Player普通攻击1段");
                    break;
                case 2:
                    self.anim.Play("Attack_Normal_02");
                    Debug.Log("Player普通攻击2段");
                    break;
                case 3:
                    self.anim.Play("Attack_Normal_03");
                    Debug.Log("Player普通攻击3段");
                    break;
                case 4:
                    self.anim.Play("Attack_Normal_04");
                    Debug.Log("Player普通攻击4段");
                    attackCounter = 0;
                    break;
            }
        }
        if (self.isAnimationFinished == true)
        {
            self.TransState(PlayerStateType.Idle);
        }






        //用magnitude返回向量的平方根即向量的长度，通过向量的长度大于一个较小值，来判断是否存在输入
        if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
        {
            self.TransState(PlayerStateType.Move);
            Debug.Log("移动状态");
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

