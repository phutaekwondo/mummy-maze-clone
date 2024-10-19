using UnityEngine;
using System.Collections.Generic;

namespace LevelEditor
{
    public class LevelEditModeCharacters : LevelEditModeBase
    {
        [SerializeField] private CharacterMover playerMover;
        [SerializeField] private CharacterMover enemyMover;
        [SerializeField] private CellTargetManager cellTargetManager;

        private bool isDraggingChar = false;

        private List<LevelEditor.CharacterMover> characterMovers = new List<CharacterMover>();

        private void Awake()
        {
            this.characterMovers.Add(this.playerMover);
            this.characterMovers.Add(this.enemyMover);
        }

        private void OnCharMouseEnter(CharacterMover charMover)
        {
            this.cellTargetManager.SetEnable(true);
        }

        private void OnCharMouseExit(CharacterMover charMover)
        {
            if (!this.isDraggingChar)
            {
                this.cellTargetManager.SetEnable(false);
            }
        }

        private void OnCharStartBeingHeld(CharacterMover charMover)
        {
            this.isDraggingChar = true;
            this.cellTargetManager.RegisterCharacterMover(charMover);
        }

        private void OnCharStopBeingHeld(CharacterMover charMover)
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
}