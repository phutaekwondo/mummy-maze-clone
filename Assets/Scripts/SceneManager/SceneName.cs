using System.Collections.Generic;

namespace MummyMaze
{
    public enum Scene
    {
        MainMenu,
        Game,
        LevelEditor,
    }

    public static class SceneName
    {
        private static Dictionary<MummyMaze.Scene, string> sceneName = new Dictionary<MummyMaze.Scene, string>
        {
            { MummyMaze.Scene.MainMenu, "MainMenu" },
            { MummyMaze.Scene.Game, "Game" },
            { MummyMaze.Scene.LevelEditor, "LevelEditor" },
        };

        public static string GetSceneName(MummyMaze.Scene scene)
        {
            return sceneName[scene];
        }
    }
}