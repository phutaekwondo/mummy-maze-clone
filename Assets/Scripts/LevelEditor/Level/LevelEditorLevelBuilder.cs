using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class LevelEditorLevelBuilder : LevelBuilder
    {
        [SerializeField] EditExitDoorManager editExitDoorManager;
        [SerializeField] AroundWallsManager aroundWallsManager;

        protected override void BuildWalls(
            List<Vector2Int> wallsLocateOrdinate,
            int groundSize,
            int exitDoorCellIndex,
            ExitDoorType exitDoorType
        )
        {
            this.BuildAroundWalls(groundSize, exitDoorCellIndex, exitDoorType);
        }

        protected override ExitDoor SpawnExitDoor(CellOrdinate cell_1, CellOrdinate cell_2)
        {
            Wall wall = this.SpawnAWall(cell_1, cell_2, this.wallPrefab);
            wall.gameObject.SetActive(false);

            ExitDoor exitDoor = base.SpawnExitDoor(cell_1, cell_2);
            this.editExitDoorManager.ReceiveSpawnedExitDoor(exitDoor);
            this.aroundWallsManager.SetExitDoor(exitDoor);
            return exitDoor;
        }

        protected override Wall SpawnAWall(
            CellOrdinate cell_1,
            CellOrdinate cell_2,
            GameObject wallPrefab
        )
        {
            Wall newWall = base.SpawnAWall(cell_1, cell_2, wallPrefab);
            this.aroundWallsManager.AddWall(newWall);

            return newWall;
        }

        public void ClearWalls()
        {
            this.aroundWallsManager.ClearWalls();
        }
    }
}
