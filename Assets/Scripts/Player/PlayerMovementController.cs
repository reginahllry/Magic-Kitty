using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private float speed = 5f;
    
    [Header("Dashing")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCd;
    [SerializeField] private float dashCdTimer;

    [Header("Util")]
    private Vector3 moveDir;
    private TrailRenderer tr;
    private Rigidbody body;
    private bool grounded;


    void Start() 
    {
        body = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
    }
    
    void Update()
    {
        // get horizontal and vertical input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = vertical * camForward;
        Vector3 rightRelative = horizontal * camRight;

        moveDir = forwardRelative + rightRelative;

        // control player movement
        Vector3 movement = new Vector3(moveDir.x, 0, moveDir.z) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        if (dashCdTimer > 0) {
            dashCdTimer -= Time.deltaTime;
        }

        // dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;

        Vector3 applyForce = moveDir * dashForce;
        SFXManager.Play("ChloeDash", true, 0.8f);
        body.AddForce(applyForce.x, 0, applyForce.z, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        dashCdTimer = dashCd;        
    }
}
