using System.Collections.Generic;

public class LevelData
{
    public int groundSize = 0;
    public CellOrdinate playerStartPosition = new CellOrdinate(0, 0);
    public CellOrdinate enemyStartPosition = new CellOrdinate(0, 0);
    public List<BlockedCell> walls = new List<BlockedCell>();
    public BlockedCell exitDoor = new BlockedCell(new CellOrdinate(0, 0), new CellOrdinate(-1, 0));
}