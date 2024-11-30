using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class DotPageIndicator : MonoBehaviour, IPageIndicator
{
    public bool IsActive { get; private set; } = false;
    private Image image;

    private Color32 activeColor = new Color32(255, 255, 255, 255);
    private Color32 inactiveColor = new Color32(255, 255, 255, 35);

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetActive(bool active)
    {
        IsActive = active;
        image.color = active ? activeColor : inactiveColor;
    }
}
