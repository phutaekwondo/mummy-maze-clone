using UnityEngine;

public class CellPositionGetter : MonoBehaviour
{
    static public CellPositionGetter Instance;

    [SerializeField] private Ground ground;
    
    private void Awake() 
    {
        CellPositionGetter.Instance = this;
        DontDestroyOnLoad(this);
    }

    public Vector3 GetCellPosition(CellOrdinate cellOrdinate) 
    {
        return ground.GetCellPosition(cellOrdinate);
    }
}
