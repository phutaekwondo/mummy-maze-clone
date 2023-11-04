using UnityEngine;
using System;
using DigitalRuby.Tween;

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected float walkSpeed = 2;
    protected CellOrdinate cellOrdinate = new CellOrdinate(0,0);

    abstract public void MoveOneCell(EnumMoveDirection direction, Action<ITween<Vector3>> onCompleted = null);

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

    protected void RotateToMovementDirection(Vector3 direction)
    {
        Debug.Log(direction);
        this.gameObject.transform.forward = direction.normalized;
    }

    protected void TweenToPosition(Vector3 position, Action<ITween<Vector3>> onCompleted)
    {
        Action<ITween<Vector3>> updatePosition = (v) =>
        {
            this.gameObject.transform.position = v.CurrentValue;
        };

        float duration = (this.gameObject.transform.position - position).magnitude / this.walkSpeed;

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
