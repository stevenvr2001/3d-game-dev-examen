using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Make the canvas face the camera
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
