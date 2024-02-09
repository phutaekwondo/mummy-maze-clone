public class GameStateModel 
{
    private static GameStateModel instance;
    public static GameStateModel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameStateModel();
            }
            return instance;
        }
    }
}
