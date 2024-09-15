using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class AroundWallsManager : MonoBehaviour
    {
        private List<Wall> existingWalls = new List<Wall>();
        private ExitDoor exitDoor;

        public void SetExitDoor(BlockedCell blockedCell)
        {
            this.exitDoor.SetWall(blockedCell);
            for (int i = 0; i < this.existingWalls.Count; i++)
            {
                Wall wall = this.existingWalls[i];
                if (wall != this.exitDoor)
                {
                    if (wall.IsBlock(blockedCell))
                    {
                        wall.gameObject.SetActive(false);
                    }
                    else
                    {
                        wall.gameObject.SetActive(true);
                    }
                }
            }
        }

        public void AddWall(Wall wall)
        {
            this.existingWalls.Add(wall);
        }

        public void SetExitDoor(ExitDoor exitDoor)
        {
            this.exitDoor = exitDoor;
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
