using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelInfoPathText;
    [SerializeField] Button acceptButton;
    private string levelInfoPath;

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

        //check if file exists
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
            Debug.Log(levelInfo);
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
