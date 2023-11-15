using UnityEngine;
using System;
using DigitalRuby.Tween;
using Unity.VisualScripting;

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 2;
    protected CellOrdinate cellOrdinate = new CellOrdinate(0,0);
    protected EnumMoveDirection lookDirection = EnumMoveDirection.Up;
    protected Action moveOneCellCallback = null;

    abstract protected void PlayMovementAnimation();
    abstract protected void StopMovementAnimation();
    abstract protected void TurnAndMoveOneCell(EnumMoveDirection direction);

    private void Start()
    {
        this.RotateToMovementDirection(this.lookDirection);
    }

    virtual public void ActBlocked(EnumMoveDirection direction) 
    {
        this.RotateToMovementDirection(direction);
    }

    virtual protected void TweenTurn(ETurnType turnType, float duration, Action onCompleted = null)
    {
        Action totalOnCompleted = () => 
        {
            if (onCompleted != null)
            {
                onCompleted();
            }
        };

        Vector3 desEulerAngles = this.gameObject.transform.eulerAngles;
        switch(turnType) 
        {
            case ETurnType.Left:
                desEulerAngles.y -= 90;
                break;
            case ETurnType.Right:
                desEulerAngles.y += 90;
                break;
            case ETurnType.Back:
                desEulerAngles.y += 180;
                break;
            default:
                break;
        }

        this.TweenToRotation(desEulerAngles, duration, totalOnCompleted);
    }

    virtual public void MoveOneCell(EnumMoveDirection direction, Action onCompleted = null)
    {
        this.moveOneCellCallback = onCompleted;

        if (direction == this.lookDirection) 
        {
            this.MoveTowardOneCell();
            return;
        }

        this.TurnAndMoveOneCell(direction);
    }

    public CellOrdinate GetCellOrdinate()
    {
        return this.cellOrdinate;
    }

    protected void MoveTowardOneCell()
    {
        this.cellOrdinate.Move(this.lookDirection);
        Vector3 toPosition = CellTransformGetter.Instance.GetCellPosition(this.cellOrdinate);

        this.PlayMovementAnimation();

        Action<ITween<Vector3>> totalOnCompleted = (v) => 
        {
            this.CallMoveOneCellCallback();
            this.StopMovementAnimation();
        };

        this.TweenToPosition(toPosition, totalOnCompleted);
    }

    public void SetCellOrdinate(CellOrdinate cellOrdinate)
    {
        this.cellOrdinate = cellOrdinate;
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = position;
    }

    protected void CallMoveOneCellCallback()
    {
        if (this.moveOneCellCallback != null) 
        {
            this.moveOneCellCallback();
        }

        this.moveOneCellCallback = null;
    }

    protected void RotateToMovementDirection(EnumMoveDirection direction) 
    {
        if (this.lookDirection == EnumMoveDirection.None) {
            Debug.LogError("Cannot rotate Character to \'None\' direction");
            return;
        }
        
        this.lookDirection = direction;
        CellOrdinate desCell = this.cellOrdinate.GetDestinateOrdinate(direction);
        this.RotateToMovementDirection(
            CellTransformGetter.Instance.GetCellPosition(desCell)
            -
            CellTransformGetter.Instance.GetCellPosition(this.cellOrdinate)
        );
    }

    protected void RotateToMovementDirection(Vector3 direction)
    {
        this.gameObject.transform.forward = direction.normalized;
    }

    protected void TweenToRotation(Vector3 eulerAngles, float duration, Action onCompleted = null)
    {
        Action<ITween<Vector3>> updateRotation= (v) =>
        {
            this.gameObject.gameObject.transform.eulerAngles = v.CurrentValue;
        };

        Action<ITween<Vector3>> onTweenCompleted = (v) =>
        {
            if (onCompleted != null)
            {
                onCompleted();
            }
        };

        this.gameObject.gameObject.Tween(
            "RotatePlayer",
            this.gameObject.transform.eulerAngles,
            eulerAngles,
            duration,
            TweenScaleFunctions.Linear,
            updateRotation,
            onTweenCompleted
        );
    }

    protected void TweenToPosition(Vector3 position, Action<ITween<Vector3>> onCompleted)
    {
        Action<ITween<Vector3>> updatePosition = (v) =>
        {
            this.gameObject.gameObject.transform.position = v.CurrentValue;
        };

        float duration = (this.gameObject.transform.position - position).magnitude / this.movementSpeed;

        this.gameObject.gameObject.Tween(
            "MovePlayer",
            this.gameObject.transform.position,
            position,
            duration,
            TweenScaleFunctions.Linear,
            updatePosition,
            onCompleted
        );
    }
}
