using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] LevelInfo levelInfo;
    [SerializeField] LevelBuilder levelBuilder;

    private void Start() 
    {
        this.levelBuilder.BuildLevel(this.levelInfo);
    }
}
