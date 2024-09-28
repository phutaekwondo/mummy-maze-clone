using System;
using System.Collections.Generic;
using LevelEditor;
using UnityEngine;

[Serializable]
public struct LevelEditModePair
{
    public LevelEditModeType levelEditModeType;
    public LevelEditModeBase levelEditMode;
}

public class LevelEditModeManager : MonoBehaviour
{
    [SerializeField] private List<LevelEditModePair> levelEditModePairs = new List<LevelEditModePair>();
    [SerializeField] List<EditModeButton> editModeButtons = new List<EditModeButton>();

    public void Setup(LevelEditorLevel editingLevel)
    {
        for (int i = 0; i < this.levelEditModePairs.Count; i++)
        {
            this.levelEditModePairs[i].levelEditMode.Setup(editingLevel);
        }
    }

    private void Start()
    {
        this.SetupLevelEditModeButtons();
    }

    private void SetupLevelEditModeButtons()
    {
        foreach (var editModeButton in this.editModeButtons)
        {
            editModeButton.OnClickedAction = this.OnLevelEditButtonClicked;
        }
    }

    private void OnLevelEditButtonClicked(LevelEditModeType levelEditModeType)
    {
        for (int i = 0; i < this.levelEditModePairs.Count; i++)
        {
            LevelEditModeType currentLevelEditModeType = this.levelEditModePairs[i].levelEditModeType;

            if (currentLevelEditModeType == levelEditModeType)
            {
                this.levelEditModePairs[i].levelEditMode.Activate();
            }
            else
            {
                this.levelEditModePairs[i].levelEditMode.Deactivate();
            }
        }
    }
}
