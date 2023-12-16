using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransformController : MonoBehaviour
{
    public void SetPosition(CellOrdinate cellOrdinate)
    {
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = position;
    }
}
