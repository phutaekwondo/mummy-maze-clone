using System;
using System.Collections.Generic;

public class LevelInfoConverter : Converter<LevelInfo, LevelData>
{
    public LevelData Convert(LevelInfo assetLevelData)
    {
        LevelData converted = new LevelData();

        converted.groundSize = (int)assetLevelData.groundSize;
        converted.playerStartPosition = new CellOrdinate(assetLevelData.playerStartPosition.x, assetLevelData.playerStartPosition.y);
        converted.enemyStartPosition = new CellOrdinate(assetLevelData.enemyStartPosition.x, assetLevelData.enemyStartPosition.y);
        converted.walls = new List<BlockedCell>();
        for (int i = 0; i < assetLevelData.walls.Count; i++)
        {
            CellOrdinate cell_1 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(
                (int)assetLevelData.groundSize,
                assetLevelData.walls[i].x
            );

            CellOrdinate cell_2 = CellOrdinateFactory.Instance.GetCellOrdinateFromCellIndex(
                (int)assetLevelData.groundSize,
                assetLevelData.walls[i].y
            );

            converted.walls.Add(new BlockedCell(cell_1, cell_2));
        }
        converted.exitDoor = this.GetExitDoorCellOrdinates(
            (int)assetLevelData.groundSize,
            assetLevelData.exitDoorCellIndex,
            assetLevelData.exitDoorType
        );

        return converted;
    }

    private BlockedCell GetExitDoorCellOrdinates(
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

        return new BlockedCell(cell_1, cell_2);
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