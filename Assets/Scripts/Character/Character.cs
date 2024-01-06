using UnityEngine;
using DigitalRuby.Tween;
using System;
using Unity.VisualScripting;

abstract public class Character : MonoBehaviour
{
    private CharacterTransformController characterTransformController;
    private CharacterAnimController characterAnimController;

    private CellOrdinate cellOrdinate;
    private EnumMoveDirection lookDirection = EnumMoveDirection.None;

    protected virtual void Awake() 
    {
        this.characterAnimController = this.GetComponent<CharacterAnimController>();
        if (this.characterAnimController == null){
            this.characterAnimController = this.AddComponent<CharacterAnimController>();
        }
        this.characterTransformController = this.GetComponent<CharacterTransformController>();
        if (this.characterTransformController == null){
            this.characterTransformController = this.AddComponent<CharacterTransformController>();
        }
    }

    private void Start() 
    {
        this.characterAnimController.PlayIdle();
        if (this.lookDirection == EnumMoveDirection.None)
        {
            this.SetLookDirection(EnumMoveDirection.Down);
        }
    }

    public void ActBlocked(EnumMoveDirection direction)
    {
        Action onTurnComplete = () => {
            this.characterAnimController.PlayBlocked(() => {
                this.characterAnimController.PlayIdle();
            });
        };

        this.TurnToDirection(direction, onTurnComplete);
    }

    public void Move(EnumMoveDirection direction, Action onMoveComplete = null) 
    {
        Action onTurnComplete = () => {
            this.MoveToward(onMoveComplete);
        };

        this.TurnToDirection(direction, onTurnComplete);
    }

    public CellOrdinate GetCellOrdinate()
    {
        return this.cellOrdinate;
    }

    public void SetCellOrdinate(CellOrdinate cellOrdinate)
    {
        this.cellOrdinate = cellOrdinate;
        this.characterTransformController.SetPosition(cellOrdinate);
    }

    public void SetLookDirection(EnumMoveDirection direction)
    {
        this.lookDirection = direction;
        this.characterTransformController.SetEulerAngles(direction);
    }

    private void TurnToDirection(EnumMoveDirection direction, Action onTurnComplete = null)
    {
        Action callOnTurnComplete = () => {
            if (onTurnComplete != null)
            {
                onTurnComplete();
            }
        };

        if (direction == this.lookDirection)
        {
            callOnTurnComplete();
            return;
        }

        ETurnType turnType = EnumMoveDirectionHelper.GetTurnType(this.lookDirection, direction);
        Action onTurnAnimComplete = () => {
            this.SetLookDirection(direction);
            callOnTurnComplete();
        };
        this.characterAnimController.PlayTurn(turnType, onTurnAnimComplete);
    }

    private void MoveToward(Action onMoveComplete)
    {
        this.characterAnimController.PlayMove();

        CellOrdinate desCell = this.cellOrdinate.GetDestinateOrdinate(this.lookDirection);
        Action onTweenComplete = () => {
            this.cellOrdinate = desCell;
            this.characterAnimController.PlayIdle();

            if (onMoveComplete != null) 
            {
                onMoveComplete();
            }
        };
        this.characterTransformController.TweenToCell(desCell, onTweenComplete);
    }
}
