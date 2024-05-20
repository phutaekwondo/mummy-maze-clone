using UnityEditor;
using UnityEngine;

public class LoadLevelManager : MonoBehaviour
{
    public void OnBrowseButtonClicked()
    {
        string path = EditorUtility.OpenFilePanel("Load Level", "", "asset");
        if (path.Length != 0)
        {
            Debug.Log("Loading level from: " + path);
        }
    }
}
