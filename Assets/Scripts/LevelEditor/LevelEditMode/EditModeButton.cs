using System;
using UnityEngine;

public class EditModeButton : MonoBehaviour
{
    [SerializeField] private LevelEditModeType levelEditModeType;

    public Action<LevelEditModeType> OnClickedAction { private get; set; }

    public void OnClicked()
    {
        if (this.OnClickedAction != null)
        {
            this.OnClickedAction(this.levelEditModeType);
        }
    }
}
