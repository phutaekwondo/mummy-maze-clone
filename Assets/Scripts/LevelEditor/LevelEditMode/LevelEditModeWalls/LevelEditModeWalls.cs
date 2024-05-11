using System.Collections.Generic;
using LevelEditor;
using UnityEngine;

public class LevelEditModeWalls : LevelEditModeBase
{
    public GameObject visibilityChangeableWallPrefab;
    List<LevelEditor.WallBehaviour> wallBehaviours = new List<LevelEditor.WallBehaviour>();

    private void SpawnWalls(int groundWidth, int groundHeight)
    {
        this.SpawnVisibilityChangeableWalls(groundWidth, groundHeight, this.visibilityChangeableWallPrefab);
    }

    public override void Setup(EditingLevel editingLevel)
    {
        int groundSize= editingLevel.GetGroundSize();
        this.ClearWalls();
        this.SpawnWalls(groundSize, groundSize);
    }

    public override void Activate()
    {
        for (int i = 0; i < this.wallBehaviours.Count; i++)
        {
            this.wallBehaviours[i].Activate();
        }
    }

    public override void Deactivate()
    {
        for (int i = 0; i < this.wallBehaviours.Count; i++)
        {
            this.wallBehaviours[i].Deactivate();
        }
    }

    private void ClearWalls()
    {
        for (int i = 0; i < this.wallBehaviours.Count; i++)
        {
            GameObject.Destroy(this.wallBehaviours[i].gameObject);
        }

        this.wallBehaviours.Clear();
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
        Wall visibilityChangeableWall = visibilityChangeableWallGameObject.GetComponent<Wall>();
        visibilityChangeableWall.SetWall(cell_1, cell_2);

        this.wallBehaviours.Add(visibilityChangeableWallGameObject.GetComponent<LevelEditor.WallBehaviour>());
    }
}