namespace MummyMaze
{
    public class SceneManager
    {
        private static SceneManager instance;
        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        public void LoadScene(MummyMaze.Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.GetSceneName(scene));
        }
    }
}