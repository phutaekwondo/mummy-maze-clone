using UnityEngine;

public class CellTransformGetter : MonoBehaviour
{
    static public CellTransformGetter Instance;

    [SerializeField] private Ground ground;

    private void Awake()
    {
        CellTransformGetter.Instance = this;
    }

    public Vector3 GetCellPosition(CellOrdinate cellOrdinate)
    {
        return this.ground.GetCellPosition(cellOrdinate);
    }

    public Vector3 GetCellSize()
    {
        return this.ground.GetCellSize();
    }
}
