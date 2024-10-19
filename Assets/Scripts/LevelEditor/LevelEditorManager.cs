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
    private LevelInfoConverter levelInfoConverter = new LevelInfoConverter();

    private void Start()
    {
        this.SetupScene();
        this.RegisterEvents();
    }

    private void RegisterEvents()
    {
        this.createLevelManager.RegisterOnLevelCreatingFinished(this.OnLevelCreatingFinished);
        this.loadLevelManager.RegisterOnLoadLevelFinished(this.OnLoadLevelFinished);
    }

    private void SetupScene()
    {
        this.editingLevel.BuildLevel();
    }

    private void OnLoadLevelFinished(LevelInfo loadedLevelInfo)
    {
        this.editingLevel.ApplyLoadedLevelData(levelInfoConverter.Convert(loadedLevelInfo));
        this.OnFinishLevelInitialize();
    }

    private void OnLevelCreatingFinished(CreateLevelModel createLevelModel)
    {
        this.editingLevel.ApplyCreateLevelModel(createLevelModel);
        this.OnFinishLevelInitialize();
    }

    private void OnFinishLevelInitialize()
    {
        this.levelEditModeManager.Setup(this.editingLevel);
    }
}
