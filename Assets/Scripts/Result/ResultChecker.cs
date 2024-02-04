public class ResultChecker
{
    public ResultType CheckResult(Level level, CellOrdinate playerCellOrdinate, CellOrdinate enemyCellOrdinate)
    {
        if (playerCellOrdinate.Equals(enemyCellOrdinate))
        {
            return ResultType.Lose;
        }
        if (IsReachGoal(level, playerCellOrdinate))
        {
            return ResultType.Win;
        }
        else
        {
            return ResultType.None;
        }
    }

    private bool IsReachGoal(Level level, CellOrdinate cellOrdinate)
    {
        CellOrdinate exitGateCellOrdinate = level.GetGoalCellOrdinate();
        return cellOrdinate.Equals(exitGateCellOrdinate);
    }
}
