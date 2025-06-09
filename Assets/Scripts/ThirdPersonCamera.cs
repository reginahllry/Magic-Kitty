using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float distance = 6f;
    [SerializeField] private float height = -1f;
    [SerializeField] private float side = 2f;
    private Vector3 offset;
    private float yaw = 0;
    private float pitch = 10;
    private float scroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3 (side, height, -distance);
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

        // atur posisi kamera sesuai keinginan
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);

        transform.LookAt(player.position + Vector3.up * 1.5f);

    }
}
