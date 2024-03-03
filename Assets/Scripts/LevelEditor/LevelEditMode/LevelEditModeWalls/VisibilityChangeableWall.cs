using UnityEngine;

public class VisibilityChangeableWall : Wall
{
    private void OnMouseOver() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("VisibilityChangeableWall OnMouseOver");
        }
    }
}