using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 待机状态
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
            //完蛋在转身跑过程中是还在调用转向，想办法屏蔽一下,有办法了单独给转向设立一个状态不就好了
            self.TransState(PlayerStateType.TurnBack);
            Debug.Log("开始转身跑");
        }
        //用magnitude返回向量的平方根即向量的长度，通过向量的长度大于一个较小值，来判断是否存在输入
        else if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
        {
            self.TransState(PlayerStateType.Move);
            Debug.Log("切换至移动状态");
        }

        else if (self.input.PlayerBasic.Attack.WasPerformedThisFrame())
        {
            self.TransState(PlayerStateType.Attack);
            Debug.Log("切换至攻击状态");
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }


}
#endregion
