using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class EditingLevelBuilder : LevelBuilder
    {
        [SerializeField] EditExitDoorManager editExitDoorManager;
        private List<Wall> existingWalls = new List<Wall>();

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
            ExitDoor exitDoor = base.SpawnExitDoor(cell_1, cell_2);
            this.editExitDoorManager.ReceiveSpawnedExitDoor(exitDoor);
            return exitDoor;
        }

        protected override Wall SpawnAWall(
            CellOrdinate cell_1,
            CellOrdinate cell_2,
            GameObject wallPrefab
        )
        {
            Wall newWall = base.SpawnAWall(cell_1, cell_2, wallPrefab);
            this.existingWalls.Add(newWall);

            return newWall;
        }

        public void ClearWalls()
        {
            foreach (var wall in this.existingWalls)
            {
                Destroy(wall.gameObject);
            }

            this.existingWalls.Clear();
        }
    }
}
