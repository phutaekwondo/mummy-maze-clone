class BlockedCells
{
    public CellOrdinate cell_1;
    public CellOrdinate cell_2;

    public BlockedCells(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        this.cell_1 = cell_1;
        this.cell_2 = cell_2;
    }

    public bool Equals(BlockedCells otherBlockedCells)
    {
        return
        this.cell_1.Equals(otherBlockedCells.cell_1) && this.cell_2.Equals(otherBlockedCells.cell_2)
        || this.cell_1.Equals(otherBlockedCells.cell_2) && this.cell_2.Equals(otherBlockedCells.cell_1);
        ;
    }
}