using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] LevelInfo levelInfo;
    [SerializeField] LevelBuilder levelBuilder;

    public void BuildLevel() 
    {
        this.levelBuilder.BuildLevel(this.levelInfo);
    }

    public CellOrdinate GetPlayerStartPosition() 
    {
        return new CellOrdinate(this.levelInfo.playerStartPosition.x, this.levelInfo.playerStartPosition.y);
    }
}
