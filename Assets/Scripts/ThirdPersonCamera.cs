using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float scrollSpeed = 2f;
    private float distance = 8f;
    private float height = -1f;
    private Vector3 offset;
    private float yaw = 0;
    private float pitch = 10;
    private float scroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = new Vector3 (0, height, -distance);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // ambil input pergerakan (rotasi) dari mouse
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // batasi sudut perputaran camera
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        // hitung rotasi kamera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Zoom();
        // atur posisi kamera sesuai keinginan
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);

        transform.LookAt(player.position + Vector3.up * 1.5f);

    }

    void Zoom() {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f ) {
            offset.z = offset.z + scroll;
        }
    }
}
