using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    [Header("Sounds")]
    private bool playingFootsteps = false;
    [SerializeField] private float footstepSpeed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"[{name}] AnimationStateController started with ID: {GetInstanceID()}");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    void Walk()
    {
        bool walkFront = Input.GetKey(KeyCode.S);
        animator.SetBool("walkFront", walkFront);

        bool walkBack = Input.GetKey(KeyCode.W);
        animator.SetBool("walkBack", walkBack);

        bool walkLeft = Input.GetKey(KeyCode.A);
        animator.SetBool("walkLeft", walkLeft);

        bool walkRight = Input.GetKey(KeyCode.D);
        animator.SetBool("walkRight", walkRight);

        // only start footsteps if walking and havent playedfootsteps
        if ((walkFront | walkBack | walkLeft | walkRight) && !playingFootsteps)
        {
            StartFootsteps();
        }

        // stop if not walking
        else if (!walkFront && !walkBack && !walkLeft && !walkRight && playingFootsteps)
        {
            StopFootsteps();
        }
    }

    void PlayFootsteps()
    {
        Debug.Log("Playing footsteps");
        SFXManager.Play("GrassFootsteps", true);
    }

    void StartFootsteps()
    {
        Debug.Log("Start footsteps with Speed: "+footstepSpeed);
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootsteps), 0f, footstepSpeed);
    }

    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootsteps));
    }
}
