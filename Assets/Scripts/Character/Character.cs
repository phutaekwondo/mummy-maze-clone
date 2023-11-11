using UnityEngine;
using System;
using DigitalRuby.Tween;

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 2;
    protected CellOrdinate cellOrdinate = new CellOrdinate(0,0);

    abstract protected void PlayMovementAnimation();
    abstract protected void StopMovementAnimation();
    virtual public void ActBlocked(EnumMoveDirection direction) 
    {
        this.RotateToMovementDirection(direction);
    }

    virtual public void MoveOneCell(EnumMoveDirection direction, Action onCompleted = null)
    {
        this.cellOrdinate.Move(direction);
        Vector3 toPosition = CellTransformGetter.Instance.GetCellPosition(this.cellOrdinate);

        this.PlayMovementAnimation();
        Action<ITween<Vector3>> totalOnCompleted = (v) => 
        {
            if (onCompleted != null)
            {
                onCompleted();
            }
            this.StopMovementAnimation();
        };

        this.RotateToMovementDirection(toPosition - this.gameObject.transform.position);
        this.TweenToPosition(toPosition, totalOnCompleted);
    }

    public CellOrdinate GetCellOrdinate()
    {
        return this.cellOrdinate;
    }

    public void SetCellOrdinate(CellOrdinate cellOrdinate)
    {
        this.cellOrdinate = cellOrdinate;
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = position;
    }

    protected void RotateToMovementDirection(EnumMoveDirection direction) 
    {
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

    protected void TweenToPosition(Vector3 position, Action<ITween<Vector3>> onCompleted)
    {
        Action<ITween<Vector3>> updatePosition = (v) =>
        {
            this.gameObject.transform.position = v.CurrentValue;
        };

        float duration = (this.gameObject.transform.position - position).magnitude / this.movementSpeed;

        this.gameObject.Tween(
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
