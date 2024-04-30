using System.Collections.Generic;
using LevelEditor;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private LevelEditor.EditingLevel editingLevel;
    [SerializeField] private LevelEditor.CharacterMover playerMover;
    [SerializeField] private CellTargetManager cellTargetManager;
    [SerializeField] List<EditModeButton> editModeButtons = new List<EditModeButton>();
    [SerializeField] GameObject visibilityChangeableWallPrefab;
    [SerializeField] CreateLevelManager createLevelManager;

    Dictionary<LevelEditModeType, LevelEditMode> levelEditModes = new Dictionary<LevelEditModeType, LevelEditMode>();

    private void Start() 
    {
        this.SetupScene();
        this.SetupLevelEditModeButtons();
        this.SetupLevelEditModes();
        this.RegisterEvents();
    }

    private void RegisterEvents()
    {
        this.createLevelManager.RegisterOnLevelCreatingFinished(this.OnLevelCreatingFinished);
    }

    private void SetupLevelEditModes()
    {
        this.levelEditModes[LevelEditModeType.Characters] = new LevelEditModeCharacters(this.playerMover, this.cellTargetManager);
        int groundSize = this.editingLevel.GetGroundSize();
        this.levelEditModes[LevelEditModeType.Walls] = new LevelEditModeWalls(groundSize, groundSize, this.visibilityChangeableWallPrefab);
    }

    private void SetupLevelEditModeButtons()
    {
        foreach (var editModeButton in this.editModeButtons)
        {
            editModeButton.OnClickedAction = this.OnLevelEditButtonClicked;
        }
    }

    private void SetupScene()
    {
        this.editingLevel.BuildLevel();
        this.playerMover.SetCellOrdinate(this.editingLevel.GetPlayerStartPosition());
    }

    private void OnLevelEditButtonClicked(LevelEditModeType levelEditModeType)
    {
        foreach (var levelEditMode in this.levelEditModes.Values)
        {
            levelEditMode.Deactivate();
        }

        if (this.levelEditModes.ContainsKey(levelEditModeType))
        {
            this.levelEditModes[levelEditModeType].Activate();
        }
    }

    private void OnLevelCreatingFinished(CreateLevelModel createLevelModel)
    {
        this.editingLevel.ApplyCreateLevelModel(createLevelModel);
        this.playerMover.SetCellOrdinate(this.editingLevel.GetPlayerStartPosition());
    }
}
