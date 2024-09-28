public interface LevelDataConverter<AssetLevelDataFormat>
{
    LevelData Convert(AssetLevelDataFormat assetLevelData);
    AssetLevelDataFormat Convert(LevelData levelData);
}