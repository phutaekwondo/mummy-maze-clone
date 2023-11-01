using System;
using DigitalRuby.Tween;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2;
    private CellOrdinate cellOrdinate = new CellOrdinate(0,0);
    private YBotAnimationStateController animStateController;

    private void Awake() 
    {
        this.animStateController = this.GetComponent<YBotAnimationStateController>();
    }

    public void MoveOneCell(EnumMoveDirection direction, Action<ITween<Vector3>> onCompleted = null)
    {
        this.cellOrdinate.Move(direction);
        Vector3 toPosition = CellTransformGetter.Instance.GetCellPosition(this.cellOrdinate);
        this.WalkToPosition(toPosition, onCompleted);
    }

    public void SetCellOrdinate(CellOrdinate cellOrdinate)
    {
        this.cellOrdinate = cellOrdinate;
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = position;
    }

    private void WalkToPosition(Vector3 position, Action<ITween<Vector3>> onCompleted = null)
    {

        this.RotateForWalk(position - this.gameObject.transform.position);
        this.MoveToPosition(position, onCompleted);
        this.PlayWalkAnimation();
    }

    private void PlayWalkAnimation()
    {
        this.animStateController.StartWalk();
    }

    private void StopWalkAnimation()
    {
        this.animStateController.StartIdle();
    }

    private void MoveToPosition(Vector3 position, Action<ITween<Vector3>> onCompleted)
    {
        Action<ITween<Vector3>> updatePosition = (v) =>
        {
            this.gameObject.transform.position = v.CurrentValue;
        };

        Action<ITween<Vector3>> totalOnCompleted = (v) => 
        {
            if (onCompleted != null)
            {
                onCompleted(v);
            }
            this.StopWalkAnimation();
        };

        float duration = (this.gameObject.transform.position - position).magnitude / this.walkSpeed;

        this.gameObject.Tween(
            "MovePlayer",
            this.gameObject.transform.position,
            position,
            duration,
            TweenScaleFunctions.Linear,
            updatePosition,
            totalOnCompleted 
        );
    }

    private void RotateForWalk(Vector3 walkDirection)
    {
        this.gameObject.transform.forward = walkDirection.normalized;
    }
}
