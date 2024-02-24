using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private LevelEditor.CharacterMover playerMover;
    [SerializeField] private CellTargetManager cellTargetManager;

    private void Start() 
    {
        this.level.BuildLevel();
        this.playerMover.SetCellOrdinate(this.level.GetPlayerStartPosition());

        this.cellTargetManager.RegisterCharacterMover(this.playerMover);
    }
}
