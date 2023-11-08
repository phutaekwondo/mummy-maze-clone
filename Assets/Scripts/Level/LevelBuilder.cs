using System;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private GameObject wallPrefab;
    private LevelInfo levelInfo;
    private uint widthSteps = 5;
    private uint heightSteps = 5;

    public void BuildLevel(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
        this.widthSteps = this.heightSteps = levelInfo.groundSize;
        this.ground.SetSize(this.widthSteps, this.heightSteps);
        this.BuildWalls();
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
        Wall newWall = Instantiate(this.wallPrefab, this.gameObject.transform.parent).GetComponent<Wall>();
        newWall.SetWall(cell_1, cell_2);
    }

    private CellOrdinate Parse2CellOrdinate(int cellIndex) 
    {
        int xOrdinate = cellIndex % Convert.ToInt32(this.levelInfo.groundSize);
        int zOrdinate = cellIndex / Convert.ToInt32(this.levelInfo.groundSize);
        return new CellOrdinate(xOrdinate, zOrdinate);
    }
}
