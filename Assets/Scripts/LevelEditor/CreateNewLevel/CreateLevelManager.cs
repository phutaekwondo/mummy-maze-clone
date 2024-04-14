using UnityEngine;

namespace LevelEditor
{
    public class CreateLevelManager : MonoBehaviour
    {
        private CreateLevelModel createLevelModel = new CreateLevelModel();
        [SerializeField] private GroundSizeInputManager groundSizeInputManager;

        private void Start() 
        {
            this.groundSizeInputManager.RegisterOnGroundSizeChanged(this.OnGroundSizeChanged);
        }

        private void OnGroundSizeChanged(int groundSize)
        {
            this.createLevelModel.groundSize = groundSize;
        }
    }
}
