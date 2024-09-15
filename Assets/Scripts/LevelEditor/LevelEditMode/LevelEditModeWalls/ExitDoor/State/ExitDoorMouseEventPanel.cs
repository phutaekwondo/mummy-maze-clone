using System;
using UnityEngine;

namespace LevelEditor
{
    public class ExitDoorMouseEventPanel : MonoBehaviour
    {
        public Action onMouseDown;
        public Action onMouseUp;
        public Action onMouseEnter;
        public Action onMouseExit;

        private void OnMouseEnter()
        {
            this.onMouseEnter?.Invoke();
        }

        private void OnMouseExit()
        {
            this.onMouseExit?.Invoke();
        }

        private void OnMouseDown()
        {
            this.onMouseDown?.Invoke();
        }
        private void OnMouseUp()
        {
            this.onMouseUp?.Invoke();
        }
    }
}
