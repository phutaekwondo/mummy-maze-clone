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
        ETurnType turnType = EnumMoveDirectionHelper.GetTurnType(this.lookDirection, direction);

        Action<ETurnType, float> onEnterTurn2MoveTransition = (turnType, duration) =>
        {
            this.lookDirection = direction;
            this.animStateController.StopTurnAnim();
            this.TweenRotationByTurnType(turnType, duration);
            this.MoveToward();
        };

        this.animStateController.StartTurnThenMoveAnim(turnType, onEnterTurn2MoveTransition);
    }

    virtual protected void MoveToward()
    {
        Action onMoveTowardComplete = () =>
        {
            this.animStateController.StopMoveAnim();
            this.CallMoveCallback();
            this.SetCellOrdinate(this.cellOrdinate.GetDestinateOrdinate(this.lookDirection));
        };

        this.animStateController.StartMoveAnim();
        this.TweenPositionToward(onMoveTowardComplete);
    }

    protected void TweenRotationByTurnType(ETurnType turnType, float duration, Action onComplete = null) 
    {
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

        Action<ITween<Vector3>> updateRotation= (v) =>
        {
            this.gameObject.gameObject.transform.eulerAngles = v.CurrentValue;
        };

        Action<ITween<Vector3>> onTweenCompleted = (v) =>
        {
            if (onComplete != null)
            {
                onComplete();
            }
        };

        this.gameObject.gameObject.Tween(
            this.gameObject.ToString() + "TweenRotation",
            this.gameObject.transform.eulerAngles,
            desEulerAngles,
            duration,
            TweenScaleFunctions.Linear,
            updateRotation,
            onTweenCompleted
        );
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
            this.gameObject.ToString() + "TweenPositionToward",
            this.gameObject.transform.position,
            position,
            duration,
            TweenScaleFunctions.SineEaseInOut,
            updatePosition,
            onTweenComplete
        );
    }
}
