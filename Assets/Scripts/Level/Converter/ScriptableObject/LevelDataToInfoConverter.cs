using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataToInfoConverter : Converter<LevelData, LevelInfo>
{
    public LevelInfo Convert(LevelData source)
    {
        LevelInfo levelInfo = ScriptableObject.CreateInstance<LevelInfo>();

        levelInfo.groundSize = (uint)source.groundSize;
        levelInfo.playerStartPosition = new Vector2Int(source.playerStartPosition.x, source.playerStartPosition.y);
        levelInfo.enemyStartPosition = new Vector2Int(source.enemyStartPosition.x, source.enemyStartPosition.y);
        levelInfo.walls = GetLevelInfoWalls(source.walls, source.groundSize);
        levelInfo.exitDoorCellIndex = GetExitDoorCellIndex(source.exitDoor, source.groundSize);
        levelInfo.exitDoorType = GetExitDoorType(source.exitDoor);

        return levelInfo;
    }

    private List<Vector2Int> GetLevelInfoWalls(List<BlockedCell> walls, int groundSize)
    {
        List<Vector2Int> res = new List<Vector2Int>();
        foreach (var wall in walls)
        {
            int cellIndex_1 = GetCellIndex(wall.cell_1, groundSize);
            int cellIndex_2 = GetCellIndex(wall.cell_2, groundSize);
            Vector2Int levelInfoWall = new Vector2Int(cellIndex_1, cellIndex_2);
            res.Add(levelInfoWall);
        }

        return res;
    }

    private int GetCellIndex(CellOrdinate cellOrdinate, int groundSize)
    {
        return cellOrdinate.y * groundSize + cellOrdinate.x;
    }

    private int GetExitDoorCellIndex(BlockedCell exitDoor, int groundSize)
    {
        Func<CellOrdinate, bool> isInside = (cellOrdinate) =>
        {
            return cellOrdinate.x >= 0 && cellOrdinate.x < groundSize && cellOrdinate.y >= 0 && cellOrdinate.y < groundSize;
        };

        CellOrdinate insideCell = isInside(exitDoor.cell_1) ? exitDoor.cell_1 : exitDoor.cell_2;
        return GetCellIndex(insideCell, groundSize);
    }

    private ExitDoorType GetExitDoorType(BlockedCell exitDoor)
    {
        return exitDoor.cell_1.x == exitDoor.cell_2.x ? ExitDoorType.Horizontal : ExitDoorType.Vertical;
    }
}
