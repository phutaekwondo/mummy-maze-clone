using UnityEngine;

public abstract class LevelDataGetter : MonoBehaviour
{
    public abstract LevelData Get(LevelName name);
}