using System;
using System.Collections.Generic;
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
    private Dictionary<WallBehaviourStateType, Material> stateMaterials = new Dictionary<WallBehaviourStateType, Material>();

    public WallBehaviour()
    {
        this.stateMachine = new WallBehaviourStateMachine(WallBehaviourStateType.IdleShowing, this.OnStateChanged);
    }

    private void SetupStateMaterialsDict()
    {
        this.stateMaterials[WallBehaviourStateType.IdleShowing] = this.defaultMaterial;
        this.stateMaterials[WallBehaviourStateType.IdleHiding] = this.invisibleMaterial;
        this.stateMaterials[WallBehaviourStateType.ReadyToCreate] = this.targetToCreateMaterial;
        this.stateMaterials[WallBehaviourStateType.ReadyToRemove] = this.targetToDestroyMaterial;
    }

    private void Start() 
    {
        this.SetupStateMaterialsDict();
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

    private void ChangeMaterial(WallBehaviourStateType newState)
    {
        this.wallRenderer.sharedMaterial = this.stateMaterials[newState];
    }

    private void OnStateChanged(WallBehaviourStateType newState)
    {
        Debug.Log("change to new state " + newState);
        this.ChangeMaterial(newState);
    }
}
}