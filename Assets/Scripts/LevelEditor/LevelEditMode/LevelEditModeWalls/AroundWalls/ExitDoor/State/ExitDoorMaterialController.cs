using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor.Rendering;
using UnityEngine;

namespace LevelEditor
{
    public class ExitDoorMaterialController : MonoBehaviour
    {
        [SerializeField] List<MeshRenderer> meshRenderers;
        [SerializeField] Material defaultMaterial;
        [SerializeField] Material hoverMaterial;
        [SerializeField] Material holdMaterial;
        private Dictionary<ExitDoorState, Material> materials;

        private void Awake()
        {
            this.materials = new Dictionary<ExitDoorState, Material>
            {
                {ExitDoorState.Default, defaultMaterial},
                {ExitDoorState.Hover, hoverMaterial},
                {ExitDoorState.Hold, holdMaterial}
            };

            SetMat(ExitDoorState.Default);
            this.SetMouseEventHandler();
        }

        private void SetMouseEventHandler()
        {
            ExitDoorStateMachine stateMachine = this.GetComponent<ExitDoorStateMachine>();
            stateMachine.onStateChange += (state) => this.SetMat(state);
        }

        public void SetMat(ExitDoorState state)
        {
            Material material = this.materials[state];
            for (int i = 0; i < this.meshRenderers.Count; i++)
            {
                this.meshRenderers[i].material = material;
            }
        }
    }
}
