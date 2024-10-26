namespace LevelEditor
{
    public class LevelEditorModel
    {
        public static LevelEditorModel Instance { get; private set; } = new LevelEditorModel();
        public LevelData Data = new LevelData();
        public void Reset()
        {
            Data = new LevelData();
        }
    }
}