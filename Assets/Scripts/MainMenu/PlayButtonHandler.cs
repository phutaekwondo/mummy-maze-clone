namespace MainMenu
{
    public class PlayButtonHandler: ButtonHandler
    {
        public override void OnClick()
        {
            MummyMaze.SceneManager.Instance.LoadScene(MummyMaze.Scene.Game);
        }
    }
}
