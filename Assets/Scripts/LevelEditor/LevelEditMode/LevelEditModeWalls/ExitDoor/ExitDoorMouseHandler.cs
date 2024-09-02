using System;
using UnityEngine;

namespace LevelEditor
{
    public class ExitDoorMouseHandler : MonoBehaviour
    {
        public Action onMouseDown;
        public Action onMouseUp;
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
