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
        TurnBack,
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

}

