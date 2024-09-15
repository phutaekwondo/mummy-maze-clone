using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class AroundWallsManager : MonoBehaviour
    {
        private List<Wall> existingWalls = new List<Wall>();

        public void SetExitDoor(BlockedCell blockedCell)
        {
            Debug.Log("SetExitDoor");
        }

        public void AddWall(Wall wall)
        {
            this.existingWalls.Add(wall);
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
