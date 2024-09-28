using UnityEngine;
using System.Collections.Generic;
using LevelEditor;

public class LevelEditModeCharacters : LevelEditModeBase
{
    [SerializeField] private LevelEditor.CharacterMover playerMover;
    [SerializeField] private LevelEditor.CharacterMover enemyMover;
    [SerializeField] private CellTargetManager cellTargetManager;

    private bool isDraggingChar = false;

    private List<LevelEditor.CharacterMover> characterMovers = new List<LevelEditor.CharacterMover>();

    private void Awake()
    {
        this.characterMovers.Add(this.playerMover);
        this.characterMovers.Add(this.enemyMover);
    }

    private void OnCharMouseEnter(LevelEditor.CharacterMover charMover)
    {
        this.cellTargetManager.SetEnable(true);
    }

    private void OnCharMouseExit(LevelEditor.CharacterMover charMover)
    {
        if (!this.isDraggingChar)
        {
            this.cellTargetManager.SetEnable(false);
        }
    }

    private void OnCharStartBeingHeld(LevelEditor.CharacterMover charMover)
    {
        this.isDraggingChar = true;
        this.cellTargetManager.RegisterCharacterMover(charMover);
    }

    private void OnCharStopBeingHeld(LevelEditor.CharacterMover charMover)
    {
        this.isDraggingChar = false;
        this.StopHoldingChars();
    }

    private void StopHoldingChars()
    {
        this.cellTargetManager.UnregisterCharacterMover();
        this.cellTargetManager.SetEnable(false);
    }

    public override void Setup(LevelEditorLevel editingLevel)
    {
        this.playerMover.SetCellOrdinate(editingLevel.GetPlayerStartPosition());
        this.enemyMover.SetCellOrdinate(editingLevel.GetEnemyStartPosition());
        this.cellTargetManager.SetUpPresent(editingLevel.GetGroundCellSize());
    }

    public override void Activate()
    {
        for (int i = 0; i < this.characterMovers.Count; i++)
        {
            this.characterMovers[i].onMouseEnter = this.OnCharMouseEnter;
            this.characterMovers[i].onMouseExit = this.OnCharMouseExit;
            this.characterMovers[i].onStartBeingHeld = this.OnCharStartBeingHeld;
            this.characterMovers[i].onStopBeingHeld = this.OnCharStopBeingHeld;
        }
    }

    public override void Deactivate()
    {
        this.StopHoldingChars();
        for (int i = 0; i < this.characterMovers.Count; i++)
        {
            this.characterMovers[i].onMouseEnter = null;
            this.characterMovers[i].onMouseExit = null;
            this.characterMovers[i].onStartBeingHeld = null;
            this.characterMovers[i].onStopBeingHeld = null;
        }
    }
}