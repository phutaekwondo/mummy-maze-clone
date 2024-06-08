using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class GroundSizeInputManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text groundSizeText;
        [SerializeField] private Button decreaseButton;
        [SerializeField] private Button increaseButton;

        private Action<int> onGroundSizeChanged;

        private int groundSize = 5;
        private const int MIN_GROUND_SIZE = 2;
        private const int MAX_GROUND_SIZE = 10;

        private void Start() 
        {
            this.UpdateGroundSize(5);
            this.RegisterButtonListeners();
        }

        public void RegisterOnGroundSizeChanged(Action<int> onGroundSizeChanged)
        {
            this.onGroundSizeChanged = onGroundSizeChanged;
        }

        private void RegisterButtonListeners()
        {
            this.decreaseButton.onClick.AddListener(this.OnDecreaseButton);
            this.increaseButton.onClick.AddListener(this.OnIncreaseButton);
        }

        public void OnDecreaseButton()
        {
            int newGroundSize = this.groundSize - 1;
            if (this.IsGroundSizeValid(newGroundSize))
            {
                this.UpdateGroundSize(newGroundSize);
            }
        }

        public void OnIncreaseButton()
        {
            int newGroundSize = this.groundSize + 1;
            if (this.IsGroundSizeValid(newGroundSize))
            {
                this.UpdateGroundSize(newGroundSize);
            }
        }

        private bool IsGroundSizeValid(int groundSize)
        {
            return groundSize >= MIN_GROUND_SIZE && groundSize <= MAX_GROUND_SIZE;
        }

        private void UpdateGroundSize(int newGroundSize)
        {
            this.groundSize = newGroundSize;
            this.groundSizeText.text = this.groundSize.ToString();

            this.onGroundSizeChanged?.Invoke(this.groundSize);
        }
    }
}
