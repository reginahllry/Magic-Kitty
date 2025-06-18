using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovement pm;

    [Header("Dashing")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    [Header("Dashing")]
    [SerializeField] private float dashCd;
    [SerializeField] private float dashCdTimer;

    [Header("Input")]
    [SerializeField] KeyCode dashKey = KeyCode.Space;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Dash()
    {   
        rb.AddForce(0, 0, dashForce, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
    }
}
