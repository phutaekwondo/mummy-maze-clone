using UnityEngine;

namespace LevelEditor
{
public class CharacterMover : MonoBehaviour
{
    private bool isFollowingMouse = false;
    public void SetCellOrdinate(CellOrdinate cellOrdinate)
    {
        Vector3 position = CellTransformGetter.Instance.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = position;
    }

    private void Update() 
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.StopFollowMouse();
        }

        Debug.Log("isFollowingMouse: " + this.isFollowingMouse);
    }

    private void OnMouseOver() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.StartFollowMouse();
        }
    }

    private void StopFollowMouse() 
    {
        if (!this.isFollowingMouse)
        {
            return;
        }

        this.isFollowingMouse = false;
    }

    private void StartFollowMouse() 
    {
        if (this.isFollowingMouse)
        {
            return;
        }

        this.isFollowingMouse = true;
    }
}
}

