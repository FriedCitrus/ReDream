using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        

        animator.SetBool("IsWalking", moveInputX != 0 && Mathf.Abs(rb.velocity.y) < 0.1f);
        animator.SetBool("IsJumping", rb.velocity.y > 0.01f);
        animator.SetBool("IsFalling", rb.velocity.y < -0.1f);
    }

}

