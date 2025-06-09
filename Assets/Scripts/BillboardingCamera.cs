using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class BillboardingCamera : MonoBehaviour
{
    // [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // PlayerFollowCamera will be index 0
        // PlayerAimCamera will be index 1
    }

    private void Update()
    {

        // get the camera position
        Vector3 cameraPosition = mainCamera.transform.position;

        // rotate on y axis bcs follows player
        cameraPosition.y = transform.position.y;

        // sprite faces camera
        transform.LookAt(cameraPosition);

        // rotate 180 on y axis bcs of spriterenderer???
        transform.Rotate(0f, 180f, 0f);
    }
}
