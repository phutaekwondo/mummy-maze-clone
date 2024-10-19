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
        this.createLevelManager.RegisterOnLevelCreatingFinished(this.HandleInitLevelFinished);
        this.loadLevelManager.RegisterOnLoadLevelFinished(this.HandleInitLevelFinished);
    }

    private void SetupScene()
    {
        this.editingLevel.BuildLevel();
    }

    private void HandleInitLevelFinished(LevelData levelData)
    {
        this.editingLevel.ApplyLoadedLevelData(levelData);
        this.SetupEditModes();
    }

    private void SetupEditModes()
    {
        this.levelEditModeManager.Setup(this.editingLevel);
    }
}
