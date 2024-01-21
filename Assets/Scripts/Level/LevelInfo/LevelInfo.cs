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
    public Vector2Int exitDoorCellOrdinate;
    public ExitDoorType exitDoorType;
}
