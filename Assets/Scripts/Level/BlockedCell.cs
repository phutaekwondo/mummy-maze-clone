using Unity.Mathematics;

public class BlockedCell
{
    public CellOrdinate cell_1;
    public CellOrdinate cell_2;

    public BlockedCell(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        if (!IsAdjacentCells(cell_1, cell_2))
        {
            throw new System.Exception("Creating Wall with not Adjacent cells");
        }

        this.cell_1 = cell_1;
        this.cell_2 = cell_2;
    }

    public bool Equals(BlockedCell otherBlockedCells)
    {
        return
        this.cell_1.Equals(otherBlockedCells.cell_1) && this.cell_2.Equals(otherBlockedCells.cell_2)
        || this.cell_1.Equals(otherBlockedCells.cell_2) && this.cell_2.Equals(otherBlockedCells.cell_1);
        ;
    }

    private bool IsAdjacentCells(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        return math.abs(cell_1.x - cell_2.x) + math.abs(cell_1.y - cell_2.y) == 1;
    }
}