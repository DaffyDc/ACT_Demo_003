using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBack : State
{
    //因为发现把转向动画写在Idle和Move状态，状态判断条件不清晰动画会被打断，所以决定单独写一个状态
    //但硬写应该也能写

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
        Debug.Log("开始转身跑");

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (self.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            //self.anim.SetBool("TurnBack", false);
            //用magnitude返回向量的平方根即向量的长度，通过向量的长度大于一个较小值，来判断是否存在输入
            if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
            {
                self.TransState(PlayerStateType.Move);
                Debug.Log("切换至移动状态");
            }

            else if (self.input.PlayerBasic.Attack.WasPerformedThisFrame())
            {
                self.TransState(PlayerStateType.Attack);
                Debug.Log("切换至攻击状态");
            }

            else
            {
                self.TransState(PlayerStateType.Idle);
                Debug.Log("切换至待机状态");
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        //self.anim.SetBool("TurnBack", false);
    }


}
