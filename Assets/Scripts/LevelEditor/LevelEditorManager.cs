using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private TargetCellPresent targetCellPresent;

    private void Start() 
    {
        this.level.BuildLevel();
        this.targetCellPresent.SetSize();
    }
}
