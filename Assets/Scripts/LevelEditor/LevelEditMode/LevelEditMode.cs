namespace LevelEditor
{
    public interface LevelEditMode
    {
        public void Activate();
        public void Deactivate();
        public void Setup(EditingLevel editingLevel);
    }
}