using System.Collections.Generic;
using UnityEngine;

public enum ExitDoorType
{
    Horizontal,
    Vertical
}
[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
public class LevelInfo : ScriptableObject
{
    public uint groundSize;
    public Vector2Int playerStartPosition;
    public Vector2Int enemyStartPosition;
    public List<Vector2Int> walls;
    public int exitDoorCellIndex;
    public ExitDoorType exitDoorType;

    public LevelInfo Clone()
    {
        LevelInfo levelInfo = CreateInstance<LevelInfo>();
        levelInfo.groundSize = this.groundSize;
        levelInfo.playerStartPosition = this.playerStartPosition;
        levelInfo.enemyStartPosition = this.enemyStartPosition;
        levelInfo.walls = new List<Vector2Int>(this.walls);
        levelInfo.exitDoorCellIndex = this.exitDoorCellIndex;
        levelInfo.exitDoorType = this.exitDoorType;

        return levelInfo;
    }
}
