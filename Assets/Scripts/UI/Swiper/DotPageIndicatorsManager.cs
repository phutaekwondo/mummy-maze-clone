using UnityEngine;

public class DotPageIndicatorsManager : PageIndicatorsManager
{
    [SerializeField] private DotPageIndicator[] dotIndicators;

    private void Awake()
    {
        pageIndicators = dotIndicators;
    }
}