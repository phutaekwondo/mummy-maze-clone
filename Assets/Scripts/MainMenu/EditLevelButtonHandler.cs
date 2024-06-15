namespace MainMenu
{
    public class EditLevelButtonHandler : ButtonHandler
    {
        public override void OnClick()
        {
            MummyMaze.SceneManager.Instance.LoadScene(MummyMaze.Scene.LevelEditor);
        }
    }
}