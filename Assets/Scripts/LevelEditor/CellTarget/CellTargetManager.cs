using LevelEditor;
using UnityEngine;

public class CellTargetManager : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private GroundMouseDetect groundMouseDetect;
    [SerializeField] private TargetCellPresent targetCellPresent;

    private CharacterMover characterMover;

    public void SetEnable(bool enable)
    {
        this.targetCellPresent.SetVisible(enable);
        this.groundMouseDetect.enable = enable;
    }

    public void RegisterCharacterMover(LevelEditor.CharacterMover characterMover)
    {
        this.characterMover = characterMover;
    }

    public void UnregisterCharacterMover()
    {
        this.characterMover = null;
    }

    public void SetUpPresent(Vector3 cellSize)
    {
        this.targetCellPresent.SetSize(cellSize);
    }

    private void Start()
    {
        this.SetUpPresent(this.ground.GetCellSize());
        this.groundMouseDetect.SetOnCellOrdinateChanged(this.OnCellOrdinateChanged);

        this.SetEnable(false);
    }

    private void OnCellOrdinateChanged(CellOrdinate cellOrdinate)
    {
        if (cellOrdinate == null)
        {
            this.targetCellPresent.SetVisible(false);
            return;
        }

        this.targetCellPresent.SetVisible(true);
        this.targetCellPresent.SetCell(cellOrdinate, this.ground);
        if (this.characterMover != null)
        {
            this.characterMover.SetCellOrdinate(cellOrdinate);
        }
    }
}
