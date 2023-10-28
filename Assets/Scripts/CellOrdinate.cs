public class CellOrdinate
{
    public int x {get; private set;} = 0;
    public int y {get; private set;} = 0;

    public CellOrdinate(int x = 0, int y = 0) {
        this.x = x;
        this.y = y;
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
}
