using System.Collections.Generic;
public enum EnumMoveDirection 
{
    None,
    Up,
    Left,
    Right,
    Down
}

public class EnumMoveDirectionHelper {
    public static EnumMoveDirection TurnMoveDirection(EnumMoveDirection direction, ETurnType turnType) 
    {
        if (direction == EnumMoveDirection.None){
            return EnumMoveDirection.None;
        }

        List<EnumMoveDirection> orderedMoveDirection = new List<EnumMoveDirection> {
            EnumMoveDirection.Up,
            EnumMoveDirection.Right,
            EnumMoveDirection.Down,
            EnumMoveDirection.Left,
        };

        int moveDirIndex = orderedMoveDirection.IndexOf(direction);

        switch(turnType) 
        {
            case ETurnType.Left:
                moveDirIndex -= 1;
                break;
            case ETurnType.Right:
                moveDirIndex += 1;
                break;
            case ETurnType.Back:
                moveDirIndex += 2;
                break;
            default:
                break;
        }

        if (moveDirIndex < 0)
        {
            moveDirIndex += orderedMoveDirection.Count;
        }
        else if (moveDirIndex >= orderedMoveDirection.Count) 
        {
            moveDirIndex %= orderedMoveDirection.Count;
        }

        return orderedMoveDirection[moveDirIndex];
    }
}