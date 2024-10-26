using UnityEngine;

namespace LevelEditor
{
    public class LevelEditorManager : MonoBehaviour
    {
        [SerializeField]
        private LevelEditorLevel editingLevel;

        [SerializeField]
        private CreateLevelManager createLevelManager;

        [SerializeField]
        private LoadLevelManager loadLevelManager;

        [SerializeField]
        private LevelEditModeManager levelEditModeManager;
        private LevelDataSaver saver = new LevelInfoSaver();

        public void OnSaveButton()
        {
            saver.Save(LevelEditorModel.Instance.Data);
        }

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
            LevelEditorModel.Instance.Data = levelData;
            this.SetupEditModes();
        }

        private void SetupEditModes()
        {
            this.levelEditModeManager.Setup(this.editingLevel);
        }
    }
}