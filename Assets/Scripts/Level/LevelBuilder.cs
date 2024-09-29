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

    private List<CellOrdinate> GetExitDoorCellOrdinates(
        int groundSize,
        int exitDoorCellIndex,
        ExitDoorType exitDoorType
    )
    {
        CellOrdinate cell_1 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(
            groundSize,
            exitDoorCellIndex
        );
        if (!this.isEdgeCell(cell_1, groundSize))
        {
            throw new Exception("Exit door cell index must be on the edge of the ground!");
        }

        CellOrdinate cell_2;

        if (this.isCornerCell(cell_1, groundSize))
        {
            cell_2 = this.GetOtherExitDoorCellOrdinate(cell_1, exitDoorType);
        }
        else
        {
            ExitDoorType autoExitDoorType = ExitDoorType.Horizontal;
            if (cell_1.x == 0 || cell_1.x == groundSize - 1)
            {
                autoExitDoorType = ExitDoorType.Vertical;
            }
            cell_2 = this.GetOtherExitDoorCellOrdinate(cell_1, autoExitDoorType);
        }

        List<CellOrdinate> exitDoorCellOrdinates = new List<CellOrdinate>();
        exitDoorCellOrdinates.Add(cell_1);
        exitDoorCellOrdinates.Add(cell_2);
        return exitDoorCellOrdinates;
    }

    private CellOrdinate GetOtherExitDoorCellOrdinate(CellOrdinate cell, ExitDoorType exitDoorType)
    {
        bool isCell1LeftOrTop = cell.x == 0 || cell.y == 0;
        int offset = isCell1LeftOrTop ? -1 : 1;

        CellOrdinate otherCell =
            exitDoorType == ExitDoorType.Horizontal
                ? new CellOrdinate(cell.x, cell.y + offset)
                : new CellOrdinate(cell.x + offset, cell.y);

        return otherCell;
    }

    private bool isCornerCell(CellOrdinate cell, int groundSize)
    {
        return cell.x == 0 && cell.y == 0
            || cell.x == 0 && cell.y == groundSize - 1
            || cell.x == groundSize - 1 && cell.y == 0
            || cell.x == groundSize - 1 && cell.y == groundSize - 1;
    }

    private bool isEdgeCell(CellOrdinate cell, int groundSize)
    {
        return cell.x == 0 || cell.x == groundSize - 1 || cell.y == 0 || cell.y == groundSize - 1;
    }
}
