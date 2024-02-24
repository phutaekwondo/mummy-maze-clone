using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private LevelEditor.CharacterMover playerMover;
    [SerializeField] private CellTargetManager cellTargetManager;

    private void Start() 
    {
        this.level.BuildLevel();
        this.SetupPlayer();
    }

    private void SetupPlayer()
    {
        this.playerMover.SetCellOrdinate(this.level.GetPlayerStartPosition());
        this.playerMover.onStartBeingHeld = this.OnPlayerStartBeingHeld;
        this.playerMover.onStopBeingHeld = this.OnPlayerStopBeingHeld;
    }

    private void OnPlayerStartBeingHeld(LevelEditor.CharacterMover playerMover)
    {
        this.cellTargetManager.SetEnable(true);
        this.cellTargetManager.RegisterCharacterMover(playerMover);
    }

    private void OnPlayerStopBeingHeld(LevelEditor.CharacterMover playerMover)
    {
        this.cellTargetManager.UnregisterCharacterMover();
        this.cellTargetManager.SetEnable(false);
    }
}
