using UnityEngine;

public class Player : MonoBehaviour
{
    public void SetPosition(Vector3 position) 
    {
        this.gameObject.transform.position = position;
    }
}
