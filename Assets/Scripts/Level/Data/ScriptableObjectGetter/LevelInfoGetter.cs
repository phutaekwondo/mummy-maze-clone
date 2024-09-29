using System.Collections.Generic;
using UnityEngine;

public class LevelInfoGetter : LevelDataGetter
{
    [SerializeField] LevelInfo emptyLevelInfo;
    [SerializeField] LevelInfo level1;
    private Converter<LevelInfo, LevelData> levelInfoToLevelData = new LevelInfoConverter();
    private Dictionary<LevelName, LevelInfo> levelInfoDict;

    private void Awake()
    {
        levelInfoDict = new Dictionary<LevelName, LevelInfo>
        {
            { LevelName.Empty, emptyLevelInfo },
            { LevelName.Level_1, level1 }
        };
    }

    public override LevelData Get(LevelName name)
    {
        LevelInfo levelInfo = levelInfoDict[name];
        return levelInfoToLevelData.Convert(levelInfo);
    }
}