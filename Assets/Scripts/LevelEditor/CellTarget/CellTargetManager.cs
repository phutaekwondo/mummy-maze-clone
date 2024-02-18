using UnityEngine;

public class CellTargetManager : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private GroundMouseDetect groundMouseDetect;
    [SerializeField] private TargetCellPresent targetCellPresent;

    private void Start() 
    {
        this.targetCellPresent.SetSize(ground);
        this.groundMouseDetect.SetOnCellOrdinateChanged(this.OnCellOrdinateChanged);
    }

    private void OnCellOrdinateChanged(CellOrdinate cellOrdinate)
    {
        this.targetCellPresent.SetCell(cellOrdinate, this.ground);
    }
}
