using UnityEngine;
using DigitalRuby.Tween;
using System;

abstract public class Character : MonoBehaviour
{
    private CharacterTransformController characterTransformController;
    private CellOrdinate cellOrdinate;

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
