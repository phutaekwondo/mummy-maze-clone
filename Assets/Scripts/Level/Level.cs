using UnityEngine;
using UnityEngine.Scripting;

public class Level : MonoBehaviour
{
    [SerializeField] protected LevelBuilder levelBuilder;
    [SerializeField] protected Ground ground;
    [SerializeField] protected LevelDataGetter levelDataGetter;
    [SerializeField] protected LevelName levelName;
    protected LevelData data;

    public Vector3 GetGroundCellSize()
    {
        return this.ground.GetCellSize();
    }

    public void BuildLevel()
    {
        if (this.data == null)
        {
            data = levelDataGetter.Get(levelName);
        }

        this.levelBuilder.BuildLevel(this.data);
    }

    public CellOrdinate GetGoalCellOrdinate()
    {
        CellOrdinate exitDoorCell1 = this.data.exitDoor.cell_1;
        CellOrdinate exitDoorCell2 = this.data.exitDoor.cell_2;
        if (IsOutOfGround(exitDoorCell1))
        {
            return exitDoorCell2;
        }
        return exitDoorCell1;
    }

    public int GetGroundSize()
    {
        return this.data.groundSize;
    }

    public CellOrdinate GetPlayerStartPosition()
    {
        return this.data.playerStartPosition;
    }

    public CellOrdinate GetEnemyStartPosition()
    {
        return this.data.enemyStartPosition;
    }

    public bool IsBlocked(CellOrdinate cell, EnumMoveDirection direction)
    {
        if (direction == EnumMoveDirection.None)
        {
            return false;
        }

        CellOrdinate destinate = cell.GetDestinateOrdinate(direction);

        return this.IsBlocked(cell, destinate);
    }

    public bool IsBlocked(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        if (this.IsOutOfGround(cell_2))
        {
            return true;
        }
        BlockedCell checkingBlockedCell = new BlockedCell(cell_1, cell_2);

        for (int i = 0; i < this.data.walls.Count; i++)
        {
            if (this.data.walls[i].Equals(checkingBlockedCell))
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
            cellOrdinate.x >= this.data.groundSize ||
            cellOrdinate.y >= this.data.groundSize
        );
    }
}
