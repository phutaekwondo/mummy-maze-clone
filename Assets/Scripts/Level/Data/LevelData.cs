using System.Collections.Generic;

public class LevelData
{
    public int groundSize;
    public CellOrdinate playerStartPosition;
    public CellOrdinate enemyStartPosition;
    public List<BlockedCell> walls;
    public BlockedCell exitDoor;
}