using UnityEngine;

public class SpellFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;    
    private float yaw = 0f;
    private float rotationSpeed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0f, 1f, 2f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rotationSpeed = 2f;
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        Debug.Log("Rotation speed: " + rotationSpeed + "\nYaw: " + yaw + "\nRotation: " + rotation);

        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = desiredPosition;
    }
}
