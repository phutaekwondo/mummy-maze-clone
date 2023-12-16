using UnityEngine;
using DigitalRuby.Tween;
using System;

abstract public class Character : MonoBehaviour
{
    private CharacterTransformController characterTransformController;
    private CellOrdinate cellOrdinate;

    public void SetCellOrdinate(CellOrdinate cellOrdinate)
    {
        this.cellOrdinate = cellOrdinate;
        this.characterTransformController.SetPosition(cellOrdinate);
    }
}
