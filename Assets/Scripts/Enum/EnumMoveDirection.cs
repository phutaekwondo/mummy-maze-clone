using System.Collections.Generic;
using UnityEngine;

public enum EnumMoveDirection 
{
    None,
    Up,
    Left,
    Right,
    Down
}

public class EnumMoveDirectionHelper {
    public static ETurnType GetTurnType(EnumMoveDirection orgDirection, EnumMoveDirection newDirection)
    {
        if (orgDirection == EnumMoveDirection.None || newDirection == EnumMoveDirection.None)
        {
            throw new System.Exception("orgDirection == None or newDirection == None");
        }

        List<EnumMoveDirection> orderedMoveDirection = new List<EnumMoveDirection> {
            EnumMoveDirection.Up,
            EnumMoveDirection.Right,
            EnumMoveDirection.Down,
            EnumMoveDirection.Left,
        };

        int orgIndex = orderedMoveDirection.IndexOf(orgDirection);
        int newIndex = orderedMoveDirection.IndexOf(newDirection);

        switch(newIndex - orgIndex) 
        {
            case 1:
            case -3:
                return ETurnType.Right;
            case -1:
            case 3:
                return ETurnType.Left;
            case 2:
            case -2:
                return ETurnType.Back;
        }

        return ETurnType.None;
    }

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

    public static Vector3 GetVec3Direction(EnumMoveDirection direction) 
    {
        CellOrdinate cell_1 = new CellOrdinate(1,1);
        CellOrdinate cell_2 = cell_1.GetDestinateOrdinate(direction);

        return (CellTransformGetter.Instance.GetCellPosition(cell_2) - CellTransformGetter.Instance.GetCellPosition(cell_1)).normalized;
    }
}