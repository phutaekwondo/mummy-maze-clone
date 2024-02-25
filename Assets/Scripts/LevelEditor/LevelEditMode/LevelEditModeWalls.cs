using System.Collections.Generic;
using UnityEngine;

public class LevelEditModeWalls : LevelEditMode
{
    List<VisibilityChangeableWall> visibilityChangeableWalls = new List<VisibilityChangeableWall>();

    public LevelEditModeWalls(int groundWidth, int groundHeight, GameObject visibilityChangeableWallPrefab)
    {
        this.SpawnVisibilityChangeableWalls(groundWidth, groundHeight, visibilityChangeableWallPrefab);
    }

    public void Activate()
    {
    }

    public void Deactivate()
    {
    }

    private void SpawnVisibilityChangeableWalls(int groundWidth, int groundHeight, GameObject visibilityChangeableWallPrefab)
    {
        for (int i = 0; i < groundWidth; i++)
        {
            for (int j = 0; j < groundHeight - 1; j++)
            {
                CellOrdinate cell_1 = new CellOrdinate(i, j);
                CellOrdinate cell_2 = new CellOrdinate(i, j + 1);

                this.SpawnOneVisibilityChangeableWall(cell_1, cell_2, visibilityChangeableWallPrefab);
            }
        }

        for (int i = 0; i < groundWidth - 1; i++)
        {
            for (int j = 0; j < groundHeight; j++)
            {
                CellOrdinate cell_1 = new CellOrdinate(i, j);
                CellOrdinate cell_2 = new CellOrdinate(i + 1, j);

                this.SpawnOneVisibilityChangeableWall(cell_1, cell_2, visibilityChangeableWallPrefab);
            }
        }
    }

    private void SpawnOneVisibilityChangeableWall(CellOrdinate cell_1, CellOrdinate cell_2, GameObject visibilityChangeableWallPrefab)
    {
        GameObject visibilityChangeableWallGameObject = GameObject.Instantiate(visibilityChangeableWallPrefab);
        VisibilityChangeableWall visibilityChangeableWall = visibilityChangeableWallGameObject.GetComponent<VisibilityChangeableWall>();
        visibilityChangeableWall.SetWall(cell_1, cell_2);
    }
}