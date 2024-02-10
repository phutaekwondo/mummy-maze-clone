using System.Collections.Generic;

namespace MummyMaze
{
    public enum Scene
    {
        MainMenu,
        Game,
    }

    public static class SceneName
    {
        private static Dictionary<MummyMaze.Scene, string> sceneName = new Dictionary<MummyMaze.Scene, string>
        {
            { MummyMaze.Scene.MainMenu, "MainMenu" },
            { MummyMaze.Scene.Game, "Game" },
        };

        public static string GetSceneName(MummyMaze.Scene scene)
        {
            return sceneName[scene];
        }
    }
}