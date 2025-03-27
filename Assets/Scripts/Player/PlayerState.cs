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
    #region ����״̬
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
            //��magnitude����������ƽ�����������ĳ��ȣ�ͨ�������ĳ��ȴ���һ����Сֵ�����ж��Ƿ��������
            if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
            {
                self.TransState(PlayerStateType.Move);
                Debug.Log("�л����ƶ�״̬");
            }

            else if (self.input.PlayerBasic.Attack.ReadValue<float>() >= 0.01)
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

    #region �ƶ�״̬

    public class Move : State
    {
        public Player self;
        //�ù��캯��Լ������Player����
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
            //��magnitude����������ƽ�����������ĳ��ȣ�ͨ�������ĳ��ȴ���һ����Сֵ�����ж��Ƿ��������
            if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude <= 0.05f)
            {
                self.anim.SetBool("Run", false);
                self.TransState(PlayerStateType.Idle);
                Debug.Log("�л�������״̬");
            }

            else if(self.input.PlayerBasic.Attack.IsPressed())
            {
                self.TransState(PlayerStateType.Attack);
                Debug.Log("�л�������״̬");
            }

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

    #region ����״̬
    public class Attack : State
    {
        public Player self;

        private float attackCounter = 0;


        //�ù��캯��Լ������Player����
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
            attackCounter = 0;
            //private float _resetTimer = self.data._resetTimer;
        }


    }
    #endregion



}

