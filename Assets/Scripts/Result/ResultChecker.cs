public class ResultChecker
{
    public ResultType CheckResult(Level level, CellOrdinate playerCellOrdinate, CellOrdinate enemyCellOrdinate)
    {
        if (playerCellOrdinate.Equals(enemyCellOrdinate))
        {
            return ResultType.Lose;
        }
        else
        {
            return ResultType.None;
        }
    }

    private bool IsAtExitGate(Level level, CellOrdinate cellOrdinate)
    {
        return false;
    }
}
