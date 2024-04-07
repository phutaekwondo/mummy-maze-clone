using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private LevelEditor.CharacterMover playerMover;
    [SerializeField] private CellTargetManager cellTargetManager;
    [SerializeField] List<EditModeButton> editModeButtons = new List<EditModeButton>();
    [SerializeField] GameObject visibilityChangeableWallPrefab;

    Dictionary<LevelEditModeType, LevelEditMode> levelEditModes = new Dictionary<LevelEditModeType, LevelEditMode>();

    private void Start() 
    {
        this.SetupScene();
        this.SetupLevelEditModeButtons();
        this.SetupLevelEditModes();
    }

    private void SetupLevelEditModes()
    {
        this.levelEditModes[LevelEditModeType.Characters] = new LevelEditModeCharacters(this.playerMover, this.cellTargetManager);
        int groundSize = this.level.GetGroundSize();
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
        this.level.BuildLevel();
        this.playerMover.SetCellOrdinate(this.level.GetPlayerStartPosition());
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
}
