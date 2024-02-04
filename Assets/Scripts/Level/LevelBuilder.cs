using System;
using System.Collections.Generic;
using UnityEngine;

//TODO: do this class need to be a MonoBehaviour?
public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject exitDoorPrefab;
    private LevelInfo levelInfo;
    private uint widthSteps = 5;
    private uint heightSteps = 5;

    public void BuildLevel(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
        this.widthSteps = this.heightSteps = levelInfo.groundSize;
        this.ground.SetSize(this.widthSteps, this.heightSteps);
        this.AddExitGate();
        this.BuildWalls();
    }

    private void AddExitGate()
    {
        ExitDoor exitDoor = Instantiate(this.exitDoorPrefab, this.gameObject.transform.parent).GetComponent<ExitDoor>();
        List<CellOrdinate> exitDoorCellOrdinates = this.GetExitDoorCellOrdinates();
        exitDoor.SetWall(exitDoorCellOrdinates[0], exitDoorCellOrdinates[1]);
    }

    private void BuildWalls()
    {
        for(int i = 0; i < this.levelInfo.walls.Count; i++)
        {
            CellOrdinate cell_1 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(this.ground, this.levelInfo.walls[i].x);
            CellOrdinate cell_2 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(this.ground, this.levelInfo.walls[i].y);


            this.SpawnAWall(cell_1, cell_2);
        }

        this.BuildAroundWalls();
    }

    private void BuildAroundWalls() 
    {
        for (int i = 0; i < this.levelInfo.groundSize; i++)
        {
            //top
            CellOrdinate cell_in  = new CellOrdinate(i, 0);
            CellOrdinate cell_out = new CellOrdinate(i, -1);

            this.SpawnAWall(cell_in, cell_out);

            //left
            cell_in  = new CellOrdinate(0,  i);
            cell_out = new CellOrdinate(-1, i);

            this.SpawnAWall(cell_in, cell_out);

            //right
            cell_in  = new CellOrdinate((int)this.levelInfo.groundSize - 1, i);
            cell_out = new CellOrdinate((int)this.levelInfo.groundSize    , i);

            this.SpawnAWall(cell_in, cell_out);

            //bot
            cell_in  = new CellOrdinate(i, (int)this.levelInfo.groundSize - 1);
            cell_out = new CellOrdinate(i, (int)this.levelInfo.groundSize    );

            this.SpawnAWall(cell_in, cell_out);
        }
    }

    private void SpawnAWall(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        List<CellOrdinate> exitDoorCellOrdinates = this.GetExitDoorCellOrdinates();
        if (exitDoorCellOrdinates[0].Equals(cell_1) && exitDoorCellOrdinates[1].Equals(cell_2))
        {
            return;
        }
        if (exitDoorCellOrdinates[1].Equals(cell_1) && exitDoorCellOrdinates[0].Equals(cell_2))
        {
            return;
        }


        Wall newWall = Instantiate(this.wallPrefab, this.gameObject.transform.parent).GetComponent<Wall>();
        newWall.SetWall(cell_1, cell_2);
    }

    private List<CellOrdinate> GetExitDoorCellOrdinates()
    {
        CellOrdinate cell_1 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(this.ground, this.levelInfo.exitDoorCellIndex);
        if (!this.isEdgeCell(cell_1))
        {
            throw new Exception("Exit door cell index must be on the edge of the ground!");
        }

        CellOrdinate cell_2;

        if (this.isCornerCell(cell_1))
        {
            cell_2 = this.GetOtherExitDoorCellOrdinate(cell_1, this.levelInfo.exitDoorType);
        }
        else
        {
            ExitDoorType exitDoorType = ExitDoorType.Horizontal;
            if (cell_1.x == 0 || cell_1.x == this.widthSteps - 1)
            {
                exitDoorType = ExitDoorType.Vertical;
            }
            cell_2 = this.GetOtherExitDoorCellOrdinate(cell_1, exitDoorType);
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

        CellOrdinate otherCell = exitDoorType == ExitDoorType.Horizontal ?
                new CellOrdinate(cell.x, cell.y + offset) :
                new CellOrdinate(cell.x + offset, cell.y);

        return otherCell;
    }

    private bool isCornerCell(CellOrdinate cell)
    {
        return cell.x == 0 && cell.y == 0 ||
               cell.x == 0 && cell.y == this.heightSteps - 1 ||
               cell.x == this.widthSteps - 1 && cell.y == 0 ||
               cell.x == this.widthSteps - 1 && cell.y == this.heightSteps - 1;
    }

    private bool isEdgeCell(CellOrdinate cell)
    {
        return cell.x == 0 || cell.x == this.widthSteps - 1 || cell.y == 0 || cell.y == this.heightSteps - 1;
    }
}
