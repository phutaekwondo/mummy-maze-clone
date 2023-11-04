using System;
using DigitalRuby.Tween;
using UnityEngine;

public class Player : Character
{
    private YBotAnimationStateController animStateController;

    private void Awake() 
    {
        this.animStateController = this.GetComponent<YBotAnimationStateController>();
    }

    override public void MoveOneCell(EnumMoveDirection direction, Action<ITween<Vector3>> onCompleted = null)
    {
        this.cellOrdinate.Move(direction);
        Vector3 toPosition = CellTransformGetter.Instance.GetCellPosition(this.cellOrdinate);

        this.PlayWalkAnimation();
        Action<ITween<Vector3>> totalOnCompleted = (v) => 
        {
            if (onCompleted != null)
            {
                onCompleted(v);
            }
            this.StopWalkAnimation();
        };

        this.RotateToMovementDirection(toPosition - this.gameObject.transform.position);
        this.TweenToPosition(toPosition, totalOnCompleted);
    }

    private void PlayWalkAnimation()
    {
        this.animStateController.StartWalk();
    }

    private void StopWalkAnimation()
    {
        this.animStateController.StartIdle();
    }
}
