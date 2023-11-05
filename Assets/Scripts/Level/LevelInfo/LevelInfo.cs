using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
public class LevelInfo : ScriptableObject
{
    public uint groundSize {get; private set;}
    public Vector2Int playerStartPosition {get; private set;}
    public Vector2Int enemyStartPosition {get; private set;}
    public List<Vector2Int> walls {get; private set;}
}
