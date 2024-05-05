using LevelEditor;
using UnityEngine;

public abstract class LevelEditModeBase : MonoBehaviour, LevelEditMode
{
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void Setup(EditingLevel editingLevel);
}
