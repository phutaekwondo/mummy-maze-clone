using System.Collections.Generic;
using LevelEditor;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField]
    private LevelEditor.LevelEditorLevel editingLevel;

    [SerializeField]
    private CreateLevelManager createLevelManager;

    [SerializeField]
    private LoadLevelManager loadLevelManager;

    [SerializeField]
    private LevelEditModeManager levelEditModeManager;

    private void Start()
    {
        this.SetupScene();
        this.RegisterEvents();
    }

    private void RegisterEvents()
    {
        this.createLevelManager.RegisterOnLevelCreatingFinished(this.HandleLevelCreatingFinished);
        this.loadLevelManager.RegisterOnLoadLevelFinished(this.HandleLoadLevelFinished);
    }

    private void SetupScene()
    {
        this.editingLevel.BuildLevel();
    }

    private void HandleLoadLevelFinished(LevelData levelData)
    {
        this.editingLevel.ApplyLoadedLevelData(levelData);
        this.OnFinishLevelInitialize();
    }

    private void HandleLevelCreatingFinished(CreateLevelModel createLevelModel)
    {
        this.editingLevel.ApplyCreateLevelModel(createLevelModel);
        this.OnFinishLevelInitialize();
    }

    private void OnFinishLevelInitialize()
    {
        this.levelEditModeManager.Setup(this.editingLevel);
    }
}
