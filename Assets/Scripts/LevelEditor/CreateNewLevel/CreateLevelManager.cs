using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class CreateLevelManager : MonoBehaviour
    {
        private CreateLevelModel createLevelModel = new CreateLevelModel();

        private Action<CreateLevelModel> onLevelCreatingFinished;
        [SerializeField] private GameObject content;
        [SerializeField] private GroundSizeInputManager groundSizeInputManager;
        [SerializeField] private Button acceptButton;

        public void RegisterOnLevelCreatingFinished(Action<CreateLevelModel> onLevelCreatingFinished)
        {
            this.onLevelCreatingFinished = onLevelCreatingFinished;
        }

        private void Start() 
        {
            this.groundSizeInputManager.RegisterOnGroundSizeChanged(this.OnGroundSizeChanged);
            this.acceptButton.onClick.AddListener(this.OnAcceptButton);
            this.Show();
        }

        private void OnGroundSizeChanged(int groundSize)
        {
            this.createLevelModel.groundSize = groundSize;
        }

        private void Hide()
        {
            this.content.SetActive(false);
        }

        private void Show()
        {
            this.content.SetActive(true);
        }

        private void OnAcceptButton()
        {
            this.Hide();
            this.onLevelCreatingFinished?.Invoke(this.createLevelModel);
        }
    }
}
