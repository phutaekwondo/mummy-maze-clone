using LevelEditor;
using UnityEngine;

public class CellTargetManager : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private GroundMouseDetect groundMouseDetect;
    [SerializeField] private TargetCellPresent targetCellPresent;

    private CharacterMover characterMover;

    public void RegisterCharacterMover(LevelEditor.CharacterMover characterMover)
    {
        this.characterMover = characterMover;
    }

    public void UnregisterCharacterMover()
    {
        this.characterMover = null;
    }

    private void Start() 
    {
        this.targetCellPresent.SetSize(ground);
        this.groundMouseDetect.SetOnCellOrdinateChanged(this.OnCellOrdinateChanged);
    }

    private void OnCellOrdinateChanged(CellOrdinate cellOrdinate)
    {
        this.targetCellPresent.SetCell(cellOrdinate, this.ground);
        if (this.characterMover != null)
        {
            this.characterMover.SetCellOrdinate(cellOrdinate);
        }
    }
}
