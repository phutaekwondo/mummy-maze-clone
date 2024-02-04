public class CellOrdinate
{
    public int x {get; private set;} = 0;
    public int y {get; private set;} = 0;

    public CellOrdinate(int x = 0, int y = 0) {
        this.x = x;
        this.y = y;
    }

    public CellOrdinate GetDestinateOrdinate(EnumMoveDirection direction)
    {
        CellOrdinate destinate = new CellOrdinate(this.x, this.y);

        switch(direction) 
        {
            case EnumMoveDirection.Up:
                destinate.y -= 1;
                break;
            case EnumMoveDirection.Left:
                destinate.x -= 1;
                break;
            case EnumMoveDirection.Right:
                destinate.x += 1;
                break;
            case EnumMoveDirection.Down:
                destinate.y += 1;
                break;
            default: 
                break;
        }

        return destinate;
    }

    public void Move(EnumMoveDirection direction) 
    {
        switch(direction) 
        {
            case EnumMoveDirection.Up:
                this.y -= 1;
                break;
            case EnumMoveDirection.Left:
                this.x -= 1;
                break;
            case EnumMoveDirection.Right:
                this.x += 1;
                break;
            case EnumMoveDirection.Down:
                this.y += 1;
                break;
            default: 
                break;
        }
    }

    public void SetX(int x) {
        this.x = x;
    }

    public void SetY(int y) {
        this.y = y;
    }

    public bool Equals(CellOrdinate otherCell)
    {
        return this.x == otherCell.x && this.y == otherCell.y;
    }
}
