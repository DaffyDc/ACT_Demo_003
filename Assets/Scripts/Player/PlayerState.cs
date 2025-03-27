using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Windows;

namespace PlayerState
{
    public enum PlayerStateType
    {
        Idle,
        Move,
        Attack,
        Jump,
        Fall,
        Interact,
        
    }

    public class State : IPlayerState
    {
        public Player self;
        public State(Player self)
        {
            this.self = self;
        }
        public virtual void OnEnter()
        {
            self.AnimationBegin();
        }

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnExit()
        {
            self.AnimationFinish();
        }
    }
    #region 待机状态
    public class Idle : State
    {
        public Player self;

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
            //用magnitude返回向量的平方根即向量的长度，通过向量的长度大于一个较小值，来判断是否存在输入
            if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
            {
                self.TransState(PlayerStateType.Move);
                Debug.Log("切换至移动状态");
            }

            else if (self.input.PlayerBasic.Attack.ReadValue<float>() >= 0.01)
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

    #region 移动状态

    public class Move : State
    {
        public Player self;
        //用构造函数约定传入Player对象
        public Move(Player self):base(self)
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

            else if(self.input.PlayerBasic.Attack.IsPressed())
            {
                self.TransState(PlayerStateType.Attack);
                Debug.Log("切换至攻击状态");
            }

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

    #region 攻击状态
    public class Attack : State
    {
        public Player self;

        private float attackCounter = 0;


        //用构造函数约定传入Player对象
        public Attack(Player self):base(self)
        {
            this.self = self;
        }

        public override void OnEnter()
        {
            base.OnEnter();

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
            attackCounter = 0;
            //private float _resetTimer = self.data._resetTimer;
        }


    }
    #endregion



}

