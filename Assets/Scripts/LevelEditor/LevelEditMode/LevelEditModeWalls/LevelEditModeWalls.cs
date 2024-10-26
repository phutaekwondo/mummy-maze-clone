using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class LevelEditModeWalls : LevelEditModeBase
    {
        public GameObject visibilityChangeableWallPrefab;
        [SerializeField] EditExitDoorManager editExitDoorManager;
        [SerializeField] GameObject wallsParent;
        [SerializeField] ExitDoorTargetsSpawner exitDoorTargetsSpawner;
        List<WallBehaviour> wallBehaviours = new List<WallBehaviour>();

        private void SpawnWalls(int groundWidth, int groundHeight, List<BlockedCell> blockedCells)
        {
            this.SpawnWalls(groundWidth, groundHeight, blockedCells, this.visibilityChangeableWallPrefab);
        }

        public override void Setup(LevelEditorLevel editingLevel)
        {
            int groundSize = editingLevel.GetGroundSize();
            this.ClearWalls();
            this.SpawnWalls(groundSize, groundSize, editingLevel.GetBlockedCells());
            this.exitDoorTargetsSpawner.Spawn(groundSize);
        }

        public override void Activate()
        {
            for (int i = 0; i < this.wallBehaviours.Count; i++)
            {
                this.wallBehaviours[i].Activate();
            }
            this.editExitDoorManager.SetEnabled(true);
        }

        public override void Deactivate()
        {
            for (int i = 0; i < this.wallBehaviours.Count; i++)
            {
                this.wallBehaviours[i].Deactivate();
            }
            this.editExitDoorManager.SetEnabled(false);
        }

        private void ClearWalls()
        {
            for (int i = 0; i < this.wallBehaviours.Count; i++)
            {
                GameObject.Destroy(this.wallBehaviours[i].gameObject);
            }

            this.wallBehaviours.Clear();
        }

        private void SpawnWalls(
            int groundWidth,
            int groundHeight,
            List<BlockedCell> blockedCells,
            GameObject visibilityChangeableWallPrefab
        )
        {
            Func<BlockedCell, bool> calIsVisible = (blockedCell) =>
            {
                for (int i = 0; i < blockedCells.Count; i++)
                {
                    if (blockedCell.Equals(blockedCells[i]))
                    {
                        return true;
                    }
                }

                return false;
            };

            Action<CellOrdinate, CellOrdinate> spawnWall = (cell_1, cell_2) =>
            {
                BlockedCell blockedCell = new BlockedCell(cell_1, cell_2);
                bool isVisible = calIsVisible(blockedCell);
                this.SpawnOneWall(blockedCell, isVisible, visibilityChangeableWallPrefab);
            };

            for (int i = 0; i < groundWidth; i++)
            {
                for (int j = 0; j < groundHeight - 1; j++)
                {
                    CellOrdinate cell_1 = new CellOrdinate(i, j);
                    CellOrdinate cell_2 = new CellOrdinate(i, j + 1);
                    spawnWall(cell_1, cell_2);
                }
            }

            for (int i = 0; i < groundWidth - 1; i++)
            {
                for (int j = 0; j < groundHeight; j++)
                {
                    CellOrdinate cell_1 = new CellOrdinate(i, j);
                    CellOrdinate cell_2 = new CellOrdinate(i + 1, j);
                    spawnWall(cell_1, cell_2);
                }
            }
        }

        private void SpawnOneWall(
            BlockedCell blockedCell,
            bool isVisible,
            GameObject visibilityChangeableWallPrefab)
        {
            GameObject visibilityChangeableWallGameObject = GameObject.Instantiate(visibilityChangeableWallPrefab);
            Wall visibilityChangeableWall = visibilityChangeableWallGameObject.GetComponent<Wall>();
            visibilityChangeableWall.SetWall(blockedCell);
            visibilityChangeableWallGameObject.transform.SetParent(this.wallsParent.transform);

            WallBehaviour wallBehaviour = visibilityChangeableWallGameObject.GetComponent<WallBehaviour>();
            this.wallBehaviours.Add(wallBehaviour);
            wallBehaviour.onChangeVisible = HandleWallVisibleChange;
            wallBehaviour.initVisible = isVisible;
        }

        private void HandleWallVisibleChange(bool visible)
        {
            List<BlockedCell> updatedWalls = new List<BlockedCell>();
            foreach (var wallBehaviour in wallBehaviours)
            {
                if (wallBehaviour.IsVisible)
                {
                    updatedWalls.Add(wallBehaviour.BlockedCell);
                }
            }

            LevelEditorModel.Instance.Data.walls = updatedWalls;
        }
    }
}