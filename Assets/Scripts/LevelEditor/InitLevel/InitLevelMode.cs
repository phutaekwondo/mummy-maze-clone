using UnityEngine;

namespace LevelEditor
{
    public class InitLevelMode : MonoBehaviour
    {
        [SerializeField]
        protected GameObject content;

        public void Show()
        {
            this.content.SetActive(true);
        }

        public void Hide()
        {
            this.content.SetActive(false);
        }
    }
}
