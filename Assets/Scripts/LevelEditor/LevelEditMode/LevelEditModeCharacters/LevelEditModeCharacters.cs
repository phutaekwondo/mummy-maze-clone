public class LevelEditModeCharacters : LevelEditMode
{
    private LevelEditor.CharacterMover playerMover;
    private CellTargetManager cellTargetManager;

    public LevelEditModeCharacters(LevelEditor.CharacterMover playerMover, CellTargetManager cellTargetManager)
    {
        this.playerMover = playerMover;
        this.cellTargetManager = cellTargetManager;
    }

    private void OnPlayerStartBeingHeld(LevelEditor.CharacterMover playerMover)
    {
        this.cellTargetManager.SetEnable(true);
        this.cellTargetManager.RegisterCharacterMover(playerMover);
    }

    private void OnPlayerStopBeingHeld(LevelEditor.CharacterMover playerMover)
    {
        this.StopHoldingPlayer();
    }

    private void StopHoldingPlayer()
    {
        this.cellTargetManager.UnregisterCharacterMover();
        this.cellTargetManager.SetEnable(false);
    }

    public void Activate()
    {
        this.playerMover.onStartBeingHeld = this.OnPlayerStartBeingHeld;
        this.playerMover.onStopBeingHeld = this.OnPlayerStopBeingHeld;
    }

    public void Deactivate()
    {
        this.StopHoldingPlayer();
        this.playerMover.onStartBeingHeld = null;
        this.playerMover.onStopBeingHeld = null;
    }
}