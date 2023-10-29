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

            Wall newWall = Instantiate(this.wallPrefab, this.gameObject.transform.parent).GetComponent<Wall>();
            newWall.SetWall(cell_1, cell_2);
        }
    }

    private CellOrdinate Parse2CellOrdinate(int cellIndex) 
    {
        int xOrdinate = cellIndex % Convert.ToInt32(this.levelInfo.groundSize);
        int zOrdinate = cellIndex / Convert.ToInt32(this.levelInfo.groundSize);
        return new CellOrdinate(xOrdinate, zOrdinate);
    }
}
