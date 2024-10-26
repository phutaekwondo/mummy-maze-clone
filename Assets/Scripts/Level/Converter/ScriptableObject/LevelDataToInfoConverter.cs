using System;
using UnityEngine;

public class LevelDataToInfoConverter : Converter<LevelData, LevelInfo>
{
    public LevelInfo Convert(LevelData source)
    {
        //TODO: implement this
        return ScriptableObject.CreateInstance<LevelInfo>();
    }
}
