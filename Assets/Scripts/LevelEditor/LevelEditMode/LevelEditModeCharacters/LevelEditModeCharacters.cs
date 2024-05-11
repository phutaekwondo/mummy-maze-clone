using UnityEngine;
using System.Collections.Generic;
using LevelEditor;

public class LevelEditModeCharacters : LevelEditModeBase
{
    [SerializeField] private LevelEditor.CharacterMover playerMover;
    [SerializeField] private CellTargetManager cellTargetManager;

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

    public override void Setup(EditingLevel editingLevel)
    {
        this.playerMover.SetCellOrdinate(editingLevel.GetPlayerStartPosition());
        this.cellTargetManager.SetUpPresent(editingLevel.GetGroundCellSize());
    }

    public override void Activate()
    {
        this.playerMover.onStartBeingHeld = this.OnPlayerStartBeingHeld;
        this.playerMover.onStopBeingHeld = this.OnPlayerStopBeingHeld;
    }

    public override void Deactivate()
    {
        this.StopHoldingPlayer();
        this.playerMover.onStartBeingHeld = null;
        this.playerMover.onStopBeingHeld = null;
    }
}