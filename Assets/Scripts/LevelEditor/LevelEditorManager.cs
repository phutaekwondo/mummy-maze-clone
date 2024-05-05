using System.Collections.Generic;
using LevelEditor;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private LevelEditor.EditingLevel editingLevel;
    [SerializeField] CreateLevelManager createLevelManager;
    [SerializeField] LevelEditModeManager levelEditModeManager;

    private void Start() 
    {
        this.SetupScene();
        this.RegisterEvents();
    }

    private void RegisterEvents()
    {
        this.createLevelManager.RegisterOnLevelCreatingFinished(this.OnLevelCreatingFinished);
    }

    private void SetupScene()
    {
        this.editingLevel.BuildLevel();
        this.levelEditModeManager.Setup(this.editingLevel);
    }

    private void OnLevelCreatingFinished(CreateLevelModel createLevelModel)
    {
        this.editingLevel.ApplyCreateLevelModel(createLevelModel);
        this.levelEditModeManager.Setup(this.editingLevel);
    }
}
