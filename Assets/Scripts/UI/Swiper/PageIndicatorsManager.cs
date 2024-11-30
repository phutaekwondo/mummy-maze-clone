using UnityEngine;

public class PageIndicatorsManager : MonoBehaviour
{
    protected IPageIndicator[] pageIndicators;

    public int GetIndicatorsCount()
    {
        return pageIndicators.Length;
    }

    public void SetActiveIndex(int index)
    {
        if (index < 0 || index >= pageIndicators.Length)
        {
            Debug.LogError("Invalid index provided. Index must be between 0 and " + (pageIndicators.Length - 1));
            return;
        }

        for (int indicatorIndex = 0; indicatorIndex < pageIndicators.Length; indicatorIndex++)
        {
            pageIndicators[indicatorIndex].SetActive(indicatorIndex == index);
        }
    }
}