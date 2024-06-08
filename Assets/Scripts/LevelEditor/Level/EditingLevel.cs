namespace LevelEditor
{
    public class EditingLevel : Level
    {
        private LevelInfo editingLevelInfo;
        private EditingLevelBuilder editingLevelBuilder;

        private void Start()
        {
            this.editingLevelInfo = this.levelInfo.Clone();
            this.editingLevelBuilder = this.levelBuilder as EditingLevelBuilder;
        }

        public void ApplyLoadedLevelInfo(LevelInfo loadedLevelInfo)
        {
            this.levelInfo = loadedLevelInfo;
            this.editingLevelInfo = loadedLevelInfo.Clone();
            this.editingLevelBuilder.ClearWalls();
            this.BuildLevel();
        }

        public void ApplyCreateLevelModel(CreateLevelModel createLevelModel)
        {
            this.StoreCreatedLevelInfo(createLevelModel);
            this.ApplyEditingLevelInfoToScene();
        }

        private void StoreCreatedLevelInfo(CreateLevelModel createLevelModel)
        {
            this.editingLevelInfo.groundSize = (uint)createLevelModel.groundSize;
        }

        private void ApplyEditingLevelInfoToScene()
        {
            this.levelInfo = this.editingLevelInfo;
            this.editingLevelBuilder.ClearWalls();
            this.BuildLevel();
        }
    }
}
