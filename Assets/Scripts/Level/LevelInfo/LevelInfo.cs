using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
public class LevelInfo : ScriptableObject
{
    public uint groundSize;
    public Vector2Int playerStartPosition;
    public List<Vector2Int> walls;
}
