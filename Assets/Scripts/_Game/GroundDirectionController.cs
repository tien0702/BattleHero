using UnityEngine;

public class GroundDirectionController : MonoBehaviour
{
    public Transform Model;
    public Transform GroundCheck;
    void LateUpdate()
    {
        transform.forward = Model.forward;
        Vector3 newPos = GroundCheck.position;
        newPos.y = 0;
        transform.position = newPos;
    }

    
}
