using UnityEngine;
using DigitalRuby.Tween;
using System;

abstract public class Character : MonoBehaviour
{
    protected CellOrdinate cellOrdinate;
    protected EnumMoveDirection lookDirection;
    protected CharacterAnimStateController animStateController;
    [SerializeField] protected float moveSpeed = 2;
    protected Action moveCallback;

    virtual protected void Awake() 
    {
        this.animStateController = this.GetComponent<CharacterAnimStateController>();
    }

    public void SetCellOrdinate(CellOrdinate cellOrdinate)
    {
        this.cellOrdinate = cellOrdinate;
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = position;
    }

    public CellOrdinate GetCellOrdinate()
    {
        return this.cellOrdinate;
    }

    public void SetLookDirection(EnumMoveDirection direction) 
    {
        this.lookDirection = direction;
        this.gameObject.transform.forward = EnumMoveDirectionHelper.GetVec3Direction(direction);
    }

    public void Move(EnumMoveDirection direction, Action callback = null)
    {
        this.moveCallback = callback;
        if (this.lookDirection == direction) 
        {
            this.MoveToward();
        }
        else
        {
            this.Turn(direction);
        }
    }

    protected void CallMoveCallback()
    {
        if (this.moveCallback != null) 
        {
            this.moveCallback();
            this.moveCallback = null;
        }
    }

    virtual protected void Turn(EnumMoveDirection direction)
    {
    }

    virtual protected void MoveToward()
    {
        Debug.Log("Player Move Toward");

        Action onMoveTowardComplete = () =>
        {
            this.animStateController.StopMoveAnim();
            this.CallMoveCallback();
            this.SetCellOrdinate(this.cellOrdinate.GetDestinateOrdinate(this.lookDirection));
        };

        this.animStateController.StartMoveAnim();
        this.TweenPositionToward(onMoveTowardComplete);
    }

    protected void TweenPositionToward(Action onComplete = null)
    {
        Action<ITween<Vector3>> updatePosition = (v) =>
        {
            this.gameObject.gameObject.transform.position = v.CurrentValue;
        };

        Action<ITween<Vector3>> onTweenComplete = (v) => 
        {
            if (onComplete != null)
            {
                onComplete();
            }
        };

        CellOrdinate desCellOrdinate = this.cellOrdinate.GetDestinateOrdinate(this.lookDirection);
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(desCellOrdinate);

        float duration = (this.gameObject.transform.position - position).magnitude / this.moveSpeed;

        this.gameObject.gameObject.Tween(
            "TweenPositionToward",
            this.gameObject.transform.position,
            position,
            duration,
            TweenScaleFunctions.Linear,
            updatePosition,
            onTweenComplete
        );
    }
}
