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

    private void Awake() 
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

    public void Move(EnumMoveDirection direction, Action onMoveComplete = null) 
    {
        this.SetLookDirection(direction);
        this.characterAnimController.PlayMove();

        CellOrdinate desCell = this.cellOrdinate.GetDestinateOrdinate(direction);
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
}
