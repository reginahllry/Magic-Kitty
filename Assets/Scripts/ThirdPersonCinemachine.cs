using UnityEngine;

public class ThirdPersonCameraTargetController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 2f;
    public float pitchMin = -20f;
    public float pitchMax = 60f;
    private float yaw = 0f;
    private float pitch = 10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Rotate this GameObject (Follow Target)
        transform.position = player.position;
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
