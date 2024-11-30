using UnityEngine;

namespace LevelEditor
{
    public class InitLevelUIManager : MonoBehaviour
    {

        [SerializeField] PageSwiper pageSwiper;
        [SerializeField] PageIndicatorsManager indicators;

        private void SetupPage()
        {
            int startPageIndex = 0;
            pageSwiper.SetPageIndex(startPageIndex);
            indicators.SetActiveIndex(startPageIndex);

            pageSwiper.OnChangePageIndex += (newPageIndex) => indicators.SetActiveIndex(newPageIndex);
        }

        private void Start()
        {
            SetupPage();
        }
    }
}