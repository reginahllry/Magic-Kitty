using UnityEngine;

public class BillboardingCamera : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void LateUpdate()
    {
        // get the camera position
        Vector3 cameraPosition = _mainCamera.transform.position;

        // rotate on y axis bcs follows player
        cameraPosition.y = transform.position.y;

        // sprite faces camera
        transform.LookAt(cameraPosition);   

        // rotate 180 on y axis bcs of spriterenderer???
        transform.Rotate(0f, 180f, 0f);
    }
}
