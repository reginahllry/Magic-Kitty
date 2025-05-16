using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }
}
