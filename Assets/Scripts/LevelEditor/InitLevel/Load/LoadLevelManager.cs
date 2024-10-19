using System;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LoadLevelManager : InitLevelMode
    {
        [SerializeField]
        private TMP_Text levelInfoPathText;

        [SerializeField]
        Button acceptButton;
        private Action<LevelData> onLevelInfoAccepted;
        private string levelInfoPath;
        private LevelInfoConverter levelInfoConverter = new LevelInfoConverter();

        public void RegisterOnLoadLevelFinished(Action<LevelData> onLevelInfoAccepted)
        {
            this.onLevelInfoAccepted = onLevelInfoAccepted;
        }

        private void Update()
        {
            this.acceptButton.interactable = this.IsLevelInfoPathValid(this.levelInfoPath);
        }

        private bool IsLevelInfoPathValid(string levelInfoPath)
        {
            if (levelInfoPath == null || levelInfoPath.Length == 0)
            {
                return false;
            }

            return File.Exists(levelInfoPath);
        }

        public void OnAcceptButtonClicked()
        {
            string relative = FileUtil.GetProjectRelativePath(this.levelInfoPath);
            LevelInfo levelInfo = AssetDatabase.LoadAssetAtPath<LevelInfo>(relative);
            if (levelInfo == null)
            {
                Debug.LogError("LevelInfo not found at path: " + relative);
                return;
            }
            else
            {
                this.Hide();
                this.onLevelInfoAccepted?.Invoke(levelInfoConverter.Convert(levelInfo));
            }
        }

        public void OnBrowseButtonClicked()
        {
            string path = EditorUtility.OpenFilePanel("Load Level", "", "asset");
            if (path.Length != 0)
            {
                levelInfoPathText.text = path;
                this.levelInfoPath = path;
            }
        }
    }
}
