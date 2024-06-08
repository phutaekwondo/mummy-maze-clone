using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private Ground ground;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject exitDoorPrefab;

    public void BuildLevel(LevelInfo levelInfo)
    {
        this.ground.SetSize(levelInfo.groundSize, levelInfo.groundSize);
        this.BuildWalls(
            levelInfo.walls,
            (int)levelInfo.groundSize,
            levelInfo.exitDoorCellIndex,
            levelInfo.exitDoorType
        );
    }

    protected virtual void BuildWalls(
        List<Vector2Int> wallsLocateOrdinate,
        int groundSize,
        int exitDoorCellIndex,
        ExitDoorType exitDoorType
    )
    {
        for (int i = 0; i < wallsLocateOrdinate.Count; i++)
        {
            CellOrdinate cell_1 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(
                groundSize,
                wallsLocateOrdinate[i].x
            );
            CellOrdinate cell_2 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(
                groundSize,
                wallsLocateOrdinate[i].y
            );

            this.SpawnAWall(cell_1, cell_2, this.wallPrefab);
        }

        this.BuildAroundWalls(groundSize, exitDoorCellIndex, exitDoorType);
    }

    protected void BuildAroundWalls(
        int groundSize,
        int exitDoorCellIndex,
        ExitDoorType exitDoorType
    )
    {
        List<CellOrdinate> exitDoorCellOrdinates = this.GetExitDoorCellOrdinates(
            groundSize,
            exitDoorCellIndex,
            exitDoorType
        );

        Action<CellOrdinate, CellOrdinate> spawnWall = (cell_1, cell_2) =>
        {
            if (
                cell_1.Equals(exitDoorCellOrdinates[0])
                || cell_1.Equals(exitDoorCellOrdinates[1])
                || cell_2.Equals(exitDoorCellOrdinates[0])
                || cell_2.Equals(exitDoorCellOrdinates[1])
            )
            {
                this.SpawnAWall(cell_1, cell_2, this.exitDoorPrefab);
            }
            else
            {
                this.SpawnAWall(cell_1, cell_2, this.wallPrefab);
            }
        };

        for (int i = 0; i < groundSize; i++)
        {
            //top
            CellOrdinate cell_in = new CellOrdinate(i, 0);
            CellOrdinate cell_out = new CellOrdinate(i, -1);

            spawnWall(cell_in, cell_out);

            //left
            cell_in = new CellOrdinate(0, i);
            cell_out = new CellOrdinate(-1, i);

            spawnWall(cell_in, cell_out);

            //right
            cell_in = new CellOrdinate(groundSize - 1, i);
            cell_out = new CellOrdinate(groundSize, i);

            spawnWall(cell_in, cell_out);

            //bot
            cell_in = new CellOrdinate(i, groundSize - 1);
            cell_out = new CellOrdinate(i, groundSize);

            spawnWall(cell_in, cell_out);
        }
    }

    protected virtual Wall SpawnAWall(CellOrdinate cell_1, CellOrdinate cell_2, GameObject prefab)
    {
        Wall newWall = Instantiate(prefab, this.gameObject.transform.parent).GetComponent<Wall>();
        newWall.SetWall(cell_1, cell_2);

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
