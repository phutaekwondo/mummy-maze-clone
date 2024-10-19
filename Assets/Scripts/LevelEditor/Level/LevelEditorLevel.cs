using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class LevelEditorLevel : Level
    {
        private LevelEditorLevelBuilder editingLevelBuilder;

        public List<BlockedCell> GetBlockedCells()
        {
            return this.data.walls;
        }

        private void Start()
        {
            this.editingLevelBuilder = this.levelBuilder as LevelEditorLevelBuilder;
        }

        public void ApplyLoadedLevelData(LevelData data)
        {
            this.data = data;
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
            this.data.groundSize = createLevelModel.groundSize;
        }

        private void ApplyEditingLevelInfoToScene()
        {
            this.editingLevelBuilder.ClearWalls();
            this.BuildLevel();
        }
    }
}
