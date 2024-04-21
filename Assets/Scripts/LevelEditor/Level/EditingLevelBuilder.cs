using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class EditingLevelBuilder: LevelBuilder
    {
        private List<Wall> existingWalls = new List<Wall>();

        protected override Wall SpawnAWall(CellOrdinate cell_1, CellOrdinate cell_2, GameObject wallPrefab)
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