using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    public class LevelInfoSaver : LevelDataSaver
    {
        LevelDataToInfoConverter converter = new LevelDataToInfoConverter();

        public void Save(LevelData levelData)
        {
            LevelInfo levelInfo = converter.Convert(levelData);
            string filePath = EditorUtility.SaveFilePanel("Save Level Info", "", "Level_", "asset");
            if (!string.IsNullOrEmpty(filePath))
            {
                string relativePath = GetRelativePath(filePath);
                AssetDatabase.CreateAsset(levelInfo, relativePath);
                AssetDatabase.SaveAssets();
            }
        }

        private string GetRelativePath(string absolutePath)
        {
            int assetsIndex = absolutePath.IndexOf("Assets");
            string relativePath = absolutePath.Substring(assetsIndex);
            return relativePath;
        }
    }
}