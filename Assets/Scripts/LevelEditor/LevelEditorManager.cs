using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private LevelEditor.CharacterMover playerMover;

    private void Start() 
    {
        this.level.BuildLevel();
        this.playerMover.SetCellOrdinate(this.level.GetPlayerStartPosition());
    }
}
