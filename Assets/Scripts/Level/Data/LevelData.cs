using System.Collections.Generic;

public class LevelData
{
    public int groundSize = 0;
    public CellOrdinate playerStartPosition = new CellOrdinate(0, 0);
    public CellOrdinate enemyStartPosition = new CellOrdinate(0, 0);
    public List<BlockedCell> walls = new List<BlockedCell>();
    public BlockedCell exitDoor = new BlockedCell(new CellOrdinate(0, 0), new CellOrdinate(-1, 0));

    public override string ToString()
    {
        string res = "";
        res += "groundSize: " + groundSize + "\n";
        res += "play pos: " + OrdinateString(playerStartPosition) + "\n";
        res += "enemy pos: " + OrdinateString(enemyStartPosition) + "\n";
        res += "walls:\n";
        foreach (var wall in walls)
        {
            res += "\t" + OrdinateString(wall.cell_1) + " " + OrdinateString(wall.cell_2) + "\n";
        }

        res += "exitDoor: " + OrdinateString(exitDoor.cell_1) + " " + OrdinateString(exitDoor.cell_2);

        return res;
    }

    public string OrdinateString(CellOrdinate cellOrdinate)
    {
        return "(" + cellOrdinate.x + ", " + cellOrdinate.y + ")";
    }
}