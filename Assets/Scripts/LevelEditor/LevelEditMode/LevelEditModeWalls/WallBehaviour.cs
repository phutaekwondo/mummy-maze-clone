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

    private void Start() 
    {
        this.stateColorize = new WallBehaviourStateColorize(
            this.wallRenderer,
            this.defaultMaterial,
            this.invisibleMaterial,
            this.targetToCreateMaterial,
            this.targetToDestroyMaterial
        );

        this.stateMachine = new WallBehaviourStateMachine(WallBehaviourStateType.IdleHiding, this.OnStateChanged);
    }

    private void OnMouseOver()
    {
        this.stateMachine.OnMouseHover();
    }

    private void OnMouseExit()
    {
        this.stateMachine.OnMouseLeave();
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