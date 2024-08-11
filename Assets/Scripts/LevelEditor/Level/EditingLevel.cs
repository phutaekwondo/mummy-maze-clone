using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class EditingLevel : Level
    {
        private LevelInfo editingLevelInfo;
        private EditingLevelBuilder editingLevelBuilder;

        public List<BlockedCell> GetBlockedCells()
        {
            List<Vector2Int> wallsLocateOrdinate = this.editingLevelInfo.walls;
            List<BlockedCell> blockedCells = new List<BlockedCell>();

            for (int i = 0; i < wallsLocateOrdinate.Count; i++)
            {
                CellOrdinate cell_1 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(
                    (int)this.editingLevelInfo.groundSize,
                    wallsLocateOrdinate[i].x
                );
                CellOrdinate cell_2 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(
                    (int)this.editingLevelInfo.groundSize,
                    wallsLocateOrdinate[i].y
                );

                blockedCells.Add(new BlockedCell(cell_1, cell_2));
            }

            return blockedCells;
        }

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
