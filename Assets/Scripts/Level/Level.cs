using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] LevelInfo levelInfo;
    [SerializeField] LevelBuilder levelBuilder;

    public void BuildLevel() 
    {
        this.levelBuilder.BuildLevel(this.levelInfo);
    }

    public CellOrdinate GetPlayerStartPosition() 
    {
        return new CellOrdinate(this.levelInfo.playerStartPosition.x, this.levelInfo.playerStartPosition.y);
    }

    public CellOrdinate GetEnemyStartPosition() 
    {
        return new CellOrdinate(this.levelInfo.enemyStartPosition.x, this.levelInfo.enemyStartPosition.y);
    }

    public bool IsBlocked(CellOrdinate cell, EnumMoveDirection direction) 
    {
        CellOrdinate destinate = cell.GetDestinateOrdinate(direction);

        return this.IsBlocked(cell, destinate);
    }

    public bool IsBlocked(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        if (this.IsOutOfGround(cell_2)) {
            return true;
        }

        int cellIndex_1 = this.Parse2CellIndex(cell_1);
        int cellIndex_2 = this.Parse2CellIndex(cell_2);

        for (int i = 0; i < this.levelInfo.walls.Count; i++)
        {
            Vector2Int wall = this.levelInfo.walls[i];

            if ((wall.x == cellIndex_1 && wall.y == cellIndex_2) || (wall.y == cellIndex_1 && wall.x == cellIndex_2))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsOutOfGround(CellOrdinate cellOrdinate)
    {
        return (
            cellOrdinate.x < 0 ||
            cellOrdinate.y < 0 ||
            cellOrdinate.x >= this.levelInfo.groundSize ||
            cellOrdinate.y >= this.levelInfo.groundSize
        );
    }

    private int Parse2CellIndex(CellOrdinate cell) 
    {
        return cell.y * (int)this.levelInfo.groundSize + cell.x;
    }
}
