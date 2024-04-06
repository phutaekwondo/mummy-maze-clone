using System;
using UnityEngine;

namespace LevelEditor
{
    class WallBehaviourStateMachine
    {
        WallBehaviourStateType state;
        Action<WallBehaviourStateType> onEnterState;

        public WallBehaviourStateMachine(WallBehaviourStateType state, Action<WallBehaviourStateType> onEnterState = null)
        {
            this.state = state;
            this.onEnterState = onEnterState;
        }

        private void EnterState(WallBehaviourStateType state)
        {
            this.state = state;
            this.onEnterState?.Invoke(state);
        }

        public void OnMouseHover()
        {
            if (state == WallBehaviourStateType.IdleShowing)
            {
                this.EnterState(WallBehaviourStateType.ReadyToRemove);
            }
            else if (state == WallBehaviourStateType.IdleHiding)
            {
                this.EnterState(WallBehaviourStateType.ReadyToCreate);
            }
        }

        public void OnMouseLeave()
        {
            if (state == WallBehaviourStateType.ReadyToRemove)
            {
                this.EnterState(WallBehaviourStateType.IdleShowing);
            }
            else if (state == WallBehaviourStateType.ReadyToCreate)
            {
                this.EnterState(WallBehaviourStateType.IdleHiding);
            }
        }

        public void OnMouseClicked()
        {
            if (state == WallBehaviourStateType.ReadyToRemove)
            {
                this.EnterState(WallBehaviourStateType.IdleHiding);
            }
            else if (state == WallBehaviourStateType.ReadyToCreate)
            {
                this.EnterState(WallBehaviourStateType.IdleShowing);
            }
        }
    }
}