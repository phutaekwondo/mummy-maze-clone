using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    [SerializeField] private Level level;

    private void Start() 
    {
        this.level.BuildLevel();
    }
}
