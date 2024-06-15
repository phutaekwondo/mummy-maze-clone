using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;
using UnityEngine;

public class CharacterTransformController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    public void SetPosition(CellOrdinate cellOrdinate)
    {
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = position;
    }

    public void SetEulerAngles(EnumMoveDirection direction)
    {
        this.gameObject.transform.forward = EnumMoveDirectionHelper.GetVec3Direction(direction);
    }

    public void TweenToCell(CellOrdinate cellOrdinate, Action onComplete = null)
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

        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);

        float duration = (this.gameObject.transform.position - position).magnitude / this.moveSpeed;

        this.gameObject.gameObject.Tween(
            this.gameObject.ToString() + "TweenPositionToward",
            this.gameObject.transform.position,
            position,
            duration,
            TweenScaleFunctions.Linear,
            updatePosition,
            onTweenComplete
        );
    }
}
