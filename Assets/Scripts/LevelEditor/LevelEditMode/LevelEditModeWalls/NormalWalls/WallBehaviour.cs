using System;
using UnityEngine;

namespace LevelEditor
{
    [RequireComponent(typeof(Wall))]
    public class WallBehaviour : MonoBehaviour
    {
        [SerializeField] private MeshRenderer wallRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material invisibleMaterial;
        [SerializeField] private Material targetToCreateMaterial;
        [SerializeField] private Material targetToDestroyMaterial;

        public Action<bool> onChangeVisible;
        private bool isInteractable = false;
        private WallBehaviourStateMachine stateMachine;
        private WallBehaviourStateColorize stateColorize;

        public bool IsVisible
        {
            get
            {
                if (stateMachine == null)
                {
                    return false;
                }
                WallBehaviourStateType currentState = stateMachine.CurrentState;
                return currentState == WallBehaviourStateType.IdleShowing || currentState == WallBehaviourStateType.ReadyToRemove;
            }
        }

        public BlockedCell BlockedCell
        {
            get
            {
                Wall wall = GetComponent<Wall>();
                return wall.blockedCell;
            }
        }

        public bool initVisible { set; private get; } = false;

        private void Start()
        {
            this.stateColorize = new WallBehaviourStateColorize(
                this.wallRenderer,
                this.defaultMaterial,
                this.invisibleMaterial,
                this.targetToCreateMaterial,
                this.targetToDestroyMaterial
            );

            this.stateMachine = new WallBehaviourStateMachine
            (
                this.initVisible ? WallBehaviourStateType.IdleShowing : WallBehaviourStateType.IdleHiding,
                this.OnStateChanged
            );
        }

        private void OnMouseOver()
        {
            if (this.isInteractable)
            {
                this.stateMachine.OnMouseHover();
            }
        }

        private void OnMouseExit()
        {
            if (this.isInteractable)
            {
                this.stateMachine.OnMouseLeave();
            }
        }

        private void OnMouseDown()
        {
            if (this.isInteractable)
            {
                this.stateMachine.OnMouseClicked();
            }
        }

        public void Activate()
        {
            this.isInteractable = true;
        }

        public void Deactivate()
        {
            this.isInteractable = false;

            //TODO: set to default material
        }

        private void OnStateChanged(WallBehaviourStateType newState)
        {
            this.stateColorize.SetState(newState);

            if (onChangeVisible != null)
            {
                bool visible = (newState == WallBehaviourStateType.IdleShowing || newState == WallBehaviourStateType.ReadyToRemove);
                onChangeVisible(visible);
            }
        }
    }
}