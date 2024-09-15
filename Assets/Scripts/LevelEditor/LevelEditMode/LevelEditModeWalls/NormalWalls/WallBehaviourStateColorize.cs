using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    class WallBehaviourStateColorize
    {
        private Dictionary<WallBehaviourStateType, Material> stateMaterials = new Dictionary<WallBehaviourStateType, Material>();
        private MeshRenderer wallRenderer;

        public WallBehaviourStateColorize(
            MeshRenderer wallRenderer,
            Material defaultMaterial,
            Material invisibleMaterial,
            Material targetToCreateMaterial,
            Material targetToDestroyMaterial)
        {
            this.wallRenderer = wallRenderer;
            this.stateMaterials[WallBehaviourStateType.IdleShowing] = defaultMaterial;
            this.stateMaterials[WallBehaviourStateType.IdleHiding] = invisibleMaterial;
            this.stateMaterials[WallBehaviourStateType.ReadyToCreate] = targetToCreateMaterial;
            this.stateMaterials[WallBehaviourStateType.ReadyToRemove] = targetToDestroyMaterial;
        }

        public void SetState(WallBehaviourStateType state)
        {
            this.wallRenderer.material = stateMaterials[state];
        }
    }
}