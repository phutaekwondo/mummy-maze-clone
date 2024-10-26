using UnityEngine;

namespace LevelEditor
{
    public abstract class LevelEditModeBase : MonoBehaviour, LevelEditMode
    {
        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void Setup(LevelEditorLevel editingLevel);
    }
}
