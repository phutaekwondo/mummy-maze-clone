using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class LevelEditorLevelBuilder : LevelBuilder
    {
        [SerializeField] EditExitDoorManager editExitDoorManager;
        [SerializeField] AroundWallsManager aroundWallsManager;

        public override void BuildLevel(LevelData data)
        {
            if (data == null)
            {
                return;
            }

            base.BuildLevel(data);
        }

        protected override void BuildWalls(
            List<BlockedCell> walls,
            int groundSize,
            BlockedCell exitDoor
        )
        {
            this.BuildAroundWalls(groundSize, exitDoor);
        }

        protected override ExitDoor SpawnExitDoor(BlockedCell blockedCell)
        {
            Wall wall = this.SpawnAWall(blockedCell, this.wallPrefab);
            wall.gameObject.SetActive(false);

            ExitDoor exitDoor = base.SpawnExitDoor(blockedCell);
            this.editExitDoorManager.ReceiveSpawnedExitDoor(exitDoor);
            this.aroundWallsManager.SetExitDoor(exitDoor);
            return exitDoor;
        }

        protected override Wall SpawnAWall(
            BlockedCell blockedCell,
            GameObject wallPrefab
        )
        {
            Wall newWall = base.SpawnAWall(blockedCell, wallPrefab);
            this.aroundWallsManager.AddWall(newWall);

            return newWall;
        }

        public void ClearWalls()
        {
            this.aroundWallsManager.ClearWalls();
        }
    }
}
