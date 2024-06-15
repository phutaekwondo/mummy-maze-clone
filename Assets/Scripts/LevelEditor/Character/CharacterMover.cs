using System;
using Unity.VisualScripting;
using UnityEngine;

namespace LevelEditor
{
    public class CharacterMover : MonoBehaviour
    {
        private bool isBeingHeld = false;

        public Action<CharacterMover> onMouseEnter { private get; set; }
        public Action<CharacterMover> onMouseExit { private get; set; }
        public Action<CharacterMover> onStartBeingHeld { private get; set; }
        public Action<CharacterMover> onStopBeingHeld { private get; set; }

        public void SetCellOrdinate(CellOrdinate cellOrdinate)
        {
            if (cellOrdinate == null)
            {
                return;
            }

            Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
            this.gameObject.transform.position = position;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                this.StopBeingHeld();
            }
        }

        private void OnMouseEnter()
        {
            this.onMouseEnter?.Invoke(this);
        }

        private void OnMouseExit()
        {
            this.onMouseExit?.Invoke(this);
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.StartBeingHeld();
            }
        }

        private void StopBeingHeld()
        {
            if (!this.isBeingHeld)
            {
                return;
            }

            this.isBeingHeld = false;
            if (this.onStopBeingHeld != null)
            {
                this.onStopBeingHeld(this);
            }
        }

        private void StartBeingHeld()
        {
            if (this.isBeingHeld)
            {
                return;
            }

            this.isBeingHeld = true;
            if (this.onStartBeingHeld != null)
            {
                this.onStartBeingHeld(this);
            }
        }
    }
}

