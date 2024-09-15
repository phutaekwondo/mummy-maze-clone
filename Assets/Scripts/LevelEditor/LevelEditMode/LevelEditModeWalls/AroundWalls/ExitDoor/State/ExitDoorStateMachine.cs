using System;
using Unity.VisualScripting;
using UnityEngine;

namespace LevelEditor
{
    public enum ExitDoorState
    {
        Default,
        Hover,
        Hold
    }

    public class ExitDoorStateMachine : MonoBehaviour
    {
        public Action<ExitDoorState> onStateChange;
        private ExitDoorStateBehaviour stateBehaviour = new StateDefault();

        private void Awake()
        {
            this.SetMouseEventHandler();
        }

        private void SetMouseEventHandler()
        {
            ExitDoorMouseEventPanel eventPanel = this.GetComponent<ExitDoorMouseEventPanel>();

            eventPanel.onMouseEnter += this.OnMouseEnter;
            eventPanel.onMouseExit += this.OnMouseExit;
            eventPanel.onMouseDown += this.OnMouseDown;
            eventPanel.onMouseUp += this.OnMouseUp;
        }

        private void HandleStateChange(ExitDoorStateBehaviour newState)
        {
            if (this.stateBehaviour == newState)
            {
                return;
            }

            this.stateBehaviour = newState;
            this.onStateChange?.Invoke(this.stateBehaviour.state);
        }

        private void OnMouseEnter()
        {
            ExitDoorStateBehaviour newStateBehaviour = this.stateBehaviour.OnMouseEnter();
            this.HandleStateChange(newStateBehaviour);
        }

        private void OnMouseExit()
        {
            ExitDoorStateBehaviour newStateBehaviour = this.stateBehaviour.OnMouseExit();
            this.HandleStateChange(newStateBehaviour);
        }

        private void OnMouseDown()
        {
            ExitDoorStateBehaviour newStateBehaviour = this.stateBehaviour.OnMouseDown();
            this.HandleStateChange(newStateBehaviour);
        }

        private void OnMouseUp()
        {
            ExitDoorStateBehaviour newStateBehaviour = this.stateBehaviour.OnMouseUp();
            this.HandleStateChange(newStateBehaviour);
        }
    }

    abstract class ExitDoorStateBehaviour
    {
        public ExitDoorState state { get; protected set; } = ExitDoorState.Default;
        virtual public ExitDoorStateBehaviour OnMouseEnter()
        {
            return this;
        }
        virtual public ExitDoorStateBehaviour OnMouseExit()
        {
            return this;
        }
        virtual public ExitDoorStateBehaviour OnMouseDown()
        {
            return this;
        }
        virtual public ExitDoorStateBehaviour OnMouseUp()
        {
            return this;
        }
    }

    class StateDefault : ExitDoorStateBehaviour
    {
        public StateDefault()
        {
            this.state = ExitDoorState.Default;
        }

        public override ExitDoorStateBehaviour OnMouseEnter()
        {
            return new StateHover();
        }
    }

    class StateHover : ExitDoorStateBehaviour
    {
        public StateHover()
        {
            this.state = ExitDoorState.Hover;
        }

        public override ExitDoorStateBehaviour OnMouseExit()
        {
            return new StateDefault();
        }

        public override ExitDoorStateBehaviour OnMouseDown()
        {
            return new StateHold();
        }
    }

    class StateHold : ExitDoorStateBehaviour
    {
        private bool isMouseOver = true;

        public StateHold()
        {
            this.state = ExitDoorState.Hold;
        }

        public override ExitDoorStateBehaviour OnMouseUp()
        {
            if (this.isMouseOver)
            {
                return new StateHover();
            }
            else
            {
                return new StateDefault();
            }
        }

        public override ExitDoorStateBehaviour OnMouseExit()
        {
            this.isMouseOver = false;
            return this;
        }

        public override ExitDoorStateBehaviour OnMouseEnter()
        {
            this.isMouseOver = true;
            return this;
        }
    }
}
