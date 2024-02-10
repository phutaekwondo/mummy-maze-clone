using UnityEngine;

public class PlayButtonHandler: MonoBehaviour
{
    public void OnClick()
    {
        MummyMaze.SceneManager.Instance.LoadScene(MummyMaze.Scene.Game);
    }
}
