using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("References")]
    public PlayerMoveStats playerMoveStats;
    [SerializeField] private Collider2D feetcolider;
    [SerializeField] private Collider2D bodycolider;

    private Rigidbody2D rb;

    //movement variables
    private Vector2 moveVelocity;
    private bool isFacingRight;

    //collision variables
    private RaycastHit2D groundCheck;
    private RaycastHit2D headCheck;
    private bool isGrounded;
    private bool isHeadBumped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        isFacingRight = true;
    } 

    private void FixedUpdate()
    {
        CollisionCheck();

        if(isGrounded)
        {
            Move(playerMoveStats.GroundAcceleration, playerMoveStats.GroundDeceleration, InputManager.moveInput);
        }
        else
        {
            Move(playerMoveStats.AirAcceleration, playerMoveStats.AirDeceleration, InputManager.moveInput);
        }
    }

    //Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        
        if (moveInput != Vector2.zero)
        {
            FlipCheck(moveInput);
            Vector2 targetVelocity = Vector2.zero;
            targetVelocity = new Vector2(moveInput.x, rb.velocity.y)*playerMoveStats.MaxwalkSpeed;
            moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);
        } 
        else if (moveInput == Vector2.zero)
        {
            moveVelocity = Vector2.Lerp(moveVelocity, Vector2.zero, acceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);
        }
    }

    private void FlipCheck(Vector2 moveInput)
    {
        if(isFacingRight && moveInput.x <0)
        {
            Flip(false);
        }
        else if(!isFacingRight && moveInput.x > 0)
        {
            Flip(true);
        }
    }

    private void Flip(bool turnRight)
    {
        if(turnRight)
        {
            isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }

    //Collision check

    private void isGround()
    {
        Vector2 feetCastOrigin = new Vector2(feetcolider.bounds.center.x, feetcolider.bounds.min.y);
        Vector2 feetCastSize = new Vector2(feetcolider.bounds.size.x, playerMoveStats.GroundCheckDistance);

        groundCheck = Physics2D.BoxCast(feetCastOrigin, feetCastSize, 0f, Vector2.down, playerMoveStats.GroundCheckDistance, playerMoveStats.GroundLayer); 
        if (groundCheck.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }

    private void CollisionCheck()
    {
        isGround();   
    }
    

}
