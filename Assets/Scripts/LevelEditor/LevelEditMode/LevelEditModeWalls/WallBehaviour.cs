using UnityEngine;

namespace LevelEditor
{
    public class WallBehaviour : MonoBehaviour
    {
        [SerializeField] private MeshRenderer wallRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material invisibleMaterial;
        [SerializeField] private Material targetToCreateMaterial;
        [SerializeField] private Material targetToDestroyMaterial;

        private bool isInteractable = false;
        private WallBehaviourStateMachine stateMachine;
        private WallBehaviourStateColorize stateColorize;

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
        }
    }
}