using System;
using System.Collections.Generic;
using UnityEngine;

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
            CellOrdinate cell_1 = this.Parse2CellOrdinate(this.levelInfo.walls[i].x);
            CellOrdinate cell_2 = this.Parse2CellOrdinate(this.levelInfo.walls[i].y);

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

    private CellOrdinate Parse2CellOrdinate(int cellIndex) 
    {
        int xOrdinate = cellIndex % Convert.ToInt32(this.levelInfo.groundSize);
        int zOrdinate = cellIndex / Convert.ToInt32(this.levelInfo.groundSize);
        return new CellOrdinate(xOrdinate, zOrdinate);
    }

    private List<CellOrdinate> GetExitDoorCellOrdinates()
    {
        CellOrdinate cell_1 = new CellOrdinate(this.levelInfo.exitDoorCellOrdinate.x, this.levelInfo.exitDoorCellOrdinate.y);

        bool isCell1LeftOrTop = this.levelInfo.exitDoorType == ExitDoorType.Horizontal ?
                                this.levelInfo.exitDoorCellOrdinate.x == 0 :
                                this.levelInfo.exitDoorCellOrdinate.y == 0;

        int offset = isCell1LeftOrTop ? -1 : 1;

        //get the other cell, depending on the exit door type, and the cell_1
        CellOrdinate cell_2 = this.levelInfo.exitDoorType == ExitDoorType.Horizontal ?
                                new CellOrdinate(this.levelInfo.exitDoorCellOrdinate.x, this.levelInfo.exitDoorCellOrdinate.y + offset) :
                                new CellOrdinate(this.levelInfo.exitDoorCellOrdinate.x + offset, this.levelInfo.exitDoorCellOrdinate.y);
                                
        List<CellOrdinate> exitDoorCellOrdinates = new List<CellOrdinate>();
        exitDoorCellOrdinates.Add(cell_1);
        exitDoorCellOrdinates.Add(cell_2);
        return exitDoorCellOrdinates;
    }
}
