using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private Ground ground;

    [SerializeField]
    protected GameObject wallPrefab;

    [SerializeField]
    protected GameObject exitDoorPrefab;

    [SerializeField]
    private GameObject aroundWallsParent;

    public void BuildLevel(LevelData levelData)
    {
        this.ground.SetSize((uint)levelData.groundSize, (uint)levelData.groundSize);
        this.BuildWalls(
            levelData.walls,
            (int)levelData.groundSize,
            levelData.exitDoor
        );
    }

    protected virtual void BuildWalls(
        List<BlockedCell> walls,
        int groundSize,
        BlockedCell exitDoor
    )
    {
        for (int i = 0; i < walls.Count; i++)
        {
            this.SpawnAWall(walls[i], this.wallPrefab);
        }

        this.BuildAroundWalls(groundSize, exitDoor);
    }

    protected void BuildAroundWalls(
        int groundSize,
        BlockedCell exitDoor
    )
    {
        Action<BlockedCell> spawnWall = (blockedCell) =>
        {
            Wall wall;
            if (blockedCell.Equals(exitDoor))
            {
                wall = this.SpawnExitDoor(exitDoor);
            }
            else
            {
                wall = this.SpawnAWall(blockedCell, this.wallPrefab);
            }

            if (this.aroundWallsParent != null)
            {
                wall.gameObject.transform.SetParent(this.aroundWallsParent.transform);
            }
        };

        for (int i = 0; i < groundSize; i++)
        {
            //top
            CellOrdinate cell_in = new CellOrdinate(i, 0);
            CellOrdinate cell_out = new CellOrdinate(i, -1);

            spawnWall(new BlockedCell(cell_in, cell_out));

            //left
            cell_in = new CellOrdinate(0, i);
            cell_out = new CellOrdinate(-1, i);

            spawnWall(new BlockedCell(cell_in, cell_out));
            //right
            cell_in = new CellOrdinate(groundSize - 1, i);
            cell_out = new CellOrdinate(groundSize, i);

            spawnWall(new BlockedCell(cell_in, cell_out));

            //bot
            cell_in = new CellOrdinate(i, groundSize - 1);
            cell_out = new CellOrdinate(i, groundSize);

            spawnWall(new BlockedCell(cell_in, cell_out));
        }
    }

    protected virtual ExitDoor SpawnExitDoor(BlockedCell blockedCell)
    {
        Wall wall = this.SpawnAWall(blockedCell, this.exitDoorPrefab);

        return wall.GetComponent<ExitDoor>();
    }

    protected virtual Wall SpawnAWall(BlockedCell blockedCell, GameObject prefab)
    {
        Wall newWall = Instantiate(prefab, this.gameObject.transform.parent).GetComponent<Wall>();
        newWall.SetWall(blockedCell);

        return newWall;
    }
}
