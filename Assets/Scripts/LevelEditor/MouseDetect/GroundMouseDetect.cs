using UnityEngine;

public class GroundMouseDetect : MonoBehaviour
{
    [SerializeField] private Ground ground;

    private void Update() 
    {
        Vector3 mouseGroundPosition = this.GetMouseOnGround();

        Debug.Log("Mouse position: " + mouseGroundPosition);
    }

    private Vector3 GetMouseOnGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, this.ground.transform.position);

        plane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }
}
