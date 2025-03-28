using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 移动状态

public class Move : State
{
    //用构造函数约定传入Player对象
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

        //用magnitude返回向量的平方根即向量的长度，通过向量的长度大于一个较小值，来判断是否存在输入



        if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude <= 0.05f)
        {

            self.anim.SetBool("Run", false);
            self.TransState(PlayerStateType.Idle);
            Debug.Log("切换至待机状态");

        }

        else if (self.input.PlayerBasic.Attack.IsPressed())
        {
            self.TransState(PlayerStateType.Attack);
            Debug.Log("切换至攻击状态");
        }
        //Debug.Log(Vector3.Angle(self.transform.forward, Vector3.one * self.input.PlayerBasic.Move.ReadValue<Vector2>()));


        else
        {
            //一个移动的方法   self.data.speed * 
            //self.controller.Move(Time.deltaTime * self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>()));
            //self.transform.LookAt(self.transform.position + self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>()));
            //因为动画自带移动，所以这里只需要确定动画转向即可
            self.CharacterRotationControl(self.input.PlayerBasic.Move.ReadValue<Vector2>());
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }


}
#endregion
