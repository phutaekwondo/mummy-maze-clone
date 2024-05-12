using UnityEngine;
using System.Collections.Generic;
using LevelEditor;

public class LevelEditModeCharacters : LevelEditModeBase
{
    [SerializeField] private LevelEditor.CharacterMover playerMover;
    [SerializeField] private LevelEditor.CharacterMover enemyMover;
    [SerializeField] private CellTargetManager cellTargetManager;

    private void OnCharStartBeingHeld(LevelEditor.CharacterMover charMover)
    {
        this.cellTargetManager.SetEnable(true);
        this.cellTargetManager.RegisterCharacterMover(charMover);
    }

    private void OnCharStopBeingHeld(LevelEditor.CharacterMover charMover)
    {
        this.StopHoldingChars();
    }

    private void StopHoldingChars()
    {
        this.cellTargetManager.UnregisterCharacterMover();
        this.cellTargetManager.SetEnable(false);
    }

    public override void Setup(EditingLevel editingLevel)
    {
        this.playerMover.SetCellOrdinate(editingLevel.GetPlayerStartPosition());
        this.enemyMover.SetCellOrdinate(editingLevel.GetEnemyStartPosition());
        this.cellTargetManager.SetUpPresent(editingLevel.GetGroundCellSize());
    }

    public override void Activate()
    {
        this.playerMover.onStartBeingHeld = this.OnCharStartBeingHeld;
        this.playerMover.onStopBeingHeld = this.OnCharStopBeingHeld;
        this.enemyMover.onStartBeingHeld = this.OnCharStartBeingHeld;
        this.enemyMover.onStopBeingHeld = this.OnCharStopBeingHeld;
    }

    public override void Deactivate()
    {
        this.StopHoldingChars();
        this.playerMover.onStartBeingHeld = null;
        this.playerMover.onStopBeingHeld = null;
        this.enemyMover.onStartBeingHeld = null;
        this.enemyMover.onStopBeingHeld = null;
    }
}