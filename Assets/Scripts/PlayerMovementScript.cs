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

    //jump variables
    private float VerticalVelocity {get; set;}
    private bool isJumping;
    private bool isFalling;
    private bool isFastFalling;
    private float FastFallSpeed;
    private float FastFallTime;
    private int JumpsUsed;

    //apex variables
    private float ApexPoint;
    private float ApexThresholdTime;
    private bool isPastApex;
    
    //jump buffer variables
    private float jumpBufferTimer;
    private bool jumpReleasedOnBuffer;

    //jump coyote variables
    private float jumpCoyoteTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        isFacingRight = true;
    } 

    private void Update()
    {
        JumpCheck();
        CountTimers();
    }

    private void OnDrawGizmos()
    {
        if(playerMoveStats.ShowWalkJump)
        {
            DrawJumpArc(playerMoveStats.MaxwalkSpeed, Color.green);
        }
    }

    private void FixedUpdate()
    {
        CollisionCheck();
        Jump();
         
        
        if(isGrounded)
        {
            Move(playerMoveStats.GroundAcceleration, playerMoveStats.GroundDeceleration, InputManager.moveInput);
        }
        else
        {
            Move(playerMoveStats.AirAcceleration, playerMoveStats.AirDeceleration, InputManager.moveInput);
        }
    }

    #region Jump

    private void JumpCheck()
    {
        //jump button press
        if(InputManager.JumpInputPressed)
        {
            jumpBufferTimer = playerMoveStats.JumpBufferTime;
            jumpReleasedOnBuffer = false;
        }

        //jump button release
        if (InputManager.JumpInputReleased)
        {
            if (jumpBufferTimer > 0f)
            {
                jumpReleasedOnBuffer = true;
            }

            if(isJumping && VerticalVelocity > 0f)
            {
                if(isPastApex)
                {
                    isPastApex = false;
                    isFastFalling = true;
                    FastFallTime = playerMoveStats.TimeforCancel;
                    VerticalVelocity = 0f;
                }   
                else
                {
                    isFastFalling = true;
                    FastFallSpeed = VerticalVelocity;

                }
            }
        }

        //init jump
        if (jumpBufferTimer>0f && !isJumping && (isGrounded || jumpCoyoteTimer > 0f))
        {
            JumpStart(1);

            if (jumpReleasedOnBuffer)
            {
                isFastFalling = true;
                FastFallSpeed = VerticalVelocity;
            }
        }

        //double jump (optional)
        else if (jumpBufferTimer>0f && isJumping && JumpsUsed < playerMoveStats.JumpsAllowed)
        {
            isFastFalling = false;
            JumpStart(1);
        }

        //jump after coyote
        else if (jumpBufferTimer>0f && isFalling && JumpsUsed < playerMoveStats.JumpsAllowed-1)
        {
            JumpStart(2);
            isFastFalling = false;
        }

        //landing
        if ((isJumping || isFalling) && isGrounded && VerticalVelocity <= 0f)
        {
            isJumping = false;
            isFalling = false;
            isFastFalling = false;
            FastFallTime = 0f;
            JumpsUsed = 0;
            VerticalVelocity = Physics2D.gravity.y;
        }
        
    }

    private void JumpStart(int NumberOfJumps)
    {
        if(!isJumping)
        {
            isJumping = true;
        }

        jumpBufferTimer = 0f;
        JumpsUsed += NumberOfJumps;
        VerticalVelocity = playerMoveStats.JumpVelocity;
    }

    private void Jump()
    {
        //apply gravity
        if (isJumping)
        {
            //bumped head
            if(isHeadBumped)
            {
                isFastFalling = true;
            }

            //gravity on accending
            if (VerticalVelocity>=0)
            {
                //apex check
                ApexPoint = Mathf.InverseLerp(playerMoveStats.JumpVelocity, 0f, VerticalVelocity);
                if(ApexPoint > playerMoveStats.ApexThresh)
                {   
                    if(!isPastApex)
                    {
                        isPastApex = true;
                        ApexThresholdTime = 0f;
                    }

                    if(isPastApex)
                    {
                        ApexThresholdTime += Time.fixedDeltaTime;
                        if(ApexThresholdTime < playerMoveStats.ApexTime)
                        {
                            VerticalVelocity = 0f;
                        }else
                        {
                            VerticalVelocity = -0.01f;
                        }
                    }
                }
                //gravity on accending (not in apex)
                else 
                {
                    VerticalVelocity += playerMoveStats.Gravity * Time.fixedDeltaTime;
                    if(isPastApex){
                        isPastApex = false;
                    }
                }
            }

            //gravity on descending
            else if(!isFastFalling)
            {
                VerticalVelocity += playerMoveStats.Gravity * playerMoveStats.GravityReleaseMult * Time.fixedDeltaTime;
            }
            else if (VerticalVelocity < 0f)
            { 
                if(!isFalling)
                {
                    isFalling = true;
                }
            }
        }

        //jump cut
        if(isFastFalling)
        {
            if(FastFallTime >= playerMoveStats.TimeforCancel)
            {
                VerticalVelocity += playerMoveStats.Gravity * playerMoveStats.GravityReleaseMult * Time.fixedDeltaTime;
            }else if (FastFallTime < playerMoveStats.TimeforCancel)
            {
                VerticalVelocity = Mathf.Lerp(FastFallSpeed,0f,(FastFallTime/playerMoveStats.TimeforCancel));
            }
            FastFallTime += Time.fixedDeltaTime;
        }

        //normal fall
        if(!isGrounded && !isJumping)
        {
            if(!isFalling)
            {
                isFalling = true;
            }
            VerticalVelocity += playerMoveStats.Gravity* Time.fixedDeltaTime;
        }

        VerticalVelocity = Mathf.Clamp(VerticalVelocity, -playerMoveStats.MaxFallSpeed, 50f);

        rb.velocity = new Vector2(rb.velocity.x, VerticalVelocity);
    }

    #endregion

    #region Movement

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
    #endregion

    #region Collision Check

    private void isGround()
    {
        Vector2 feetCastOrigin = new Vector2(feetcolider.bounds.center.x, feetcolider.bounds.min.y);
        Vector2 feetCastSize = new Vector2(feetcolider.bounds.size.x, playerMoveStats.GroundCheckDistance);

        groundCheck = Physics2D.BoxCast(feetCastOrigin, feetCastSize, 0f, Vector2.down, playerMoveStats.GroundCheckDistance, playerMoveStats.GroundLayer); 
        if (groundCheck.collider != null)
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            isGrounded = false;
        }

    }

    private void BumpedHead()
    {
        Vector2 headCastOrigin = new Vector2(feetcolider.bounds.center.x, bodycolider.bounds.max.y);
        Vector2 headCastSize = new Vector2(feetcolider.bounds.size.x * playerMoveStats.HeadWidth, playerMoveStats.HeadCheckDistance);

        headCheck = Physics2D.BoxCast(headCastOrigin, headCastSize, 0f, Vector2.up, playerMoveStats.HeadCheckDistance, playerMoveStats.GroundLayer); 
        if (headCheck.collider != null)
        {
            isHeadBumped = true;
        }
        else
        {
            isHeadBumped = false;
        }
    }

    private void CollisionCheck()
    {
        isGround();   
    }
    #endregion
    
    #region Timers

    private void CountTimers()
    {
        jumpBufferTimer -= Time.deltaTime;

        if(!isGrounded)
        {
            jumpCoyoteTimer -= Time.deltaTime;
        }
        else{ jumpCoyoteTimer = playerMoveStats.JumpCoyoteTime; }
    }

    #endregion

    #region Debug

    private void DrawJumpArc(float moveSpeed, Color GizmoColor)
    {
        Vector2 StartPos = new Vector2(feetcolider.bounds.center.x, feetcolider.bounds.min.y);
        Vector2 LastPos = StartPos;

        float speed = 0f;
        if(playerMoveStats.DrawRight){
            speed = moveSpeed;
        }
        else
        {
            speed = -moveSpeed;
        }
        Vector2 Velocity = new Vector2(speed, playerMoveStats.JumpVelocity);

        Gizmos.color = GizmoColor;
        float timeStep = 2*playerMoveStats.TimeTillApex/playerMoveStats.JumpResolution;

        for (int i =0; i<playerMoveStats.VirtualizationSteps; i++){
            float simulation = i*timeStep;
            Vector2 displacement;
            Vector2 drawpoint;

            if(simulation < playerMoveStats.TimeTillApex)
            {
                displacement = Velocity * simulation + 0.5f * new Vector2(0, playerMoveStats.Gravity)*simulation*simulation;
            }
            else if (simulation < playerMoveStats.TimeTillApex + playerMoveStats.ApexTime)
            {
                float apextime = simulation - playerMoveStats.TimeTillApex;
                displacement = Velocity * playerMoveStats.TimeTillApex + 0.5f * new Vector2(0, playerMoveStats.Gravity) *playerMoveStats.TimeTillApex*playerMoveStats.TimeTillApex;
                displacement += new Vector2(speed, 0) * apextime;
            }
            else
            {
                float descend = simulation - (playerMoveStats.TimeTillApex + playerMoveStats.ApexTime);
                displacement = Velocity *playerMoveStats.TimeTillApex+0.5f*new Vector2(0, playerMoveStats.Gravity)*playerMoveStats.TimeTillApex*playerMoveStats.TimeTillApex;
                displacement += new Vector2(speed, 0) * playerMoveStats.ApexTime;
                displacement += new Vector2(speed, 0) * descend + 0.5f * new Vector2(0, playerMoveStats.Gravity) * descend * descend;
            }

            drawpoint = StartPos + displacement;

            if (playerMoveStats.StopOnCollision)
            {
                RaycastHit2D hit = Physics2D.Raycast(LastPos, drawpoint-LastPos,Vector2.Distance(LastPos, drawpoint), playerMoveStats.GroundLayer);
                if(hit.collider != null)
                {
                    Gizmos.DrawLine(LastPos, hit.point);
                    break;
                }
            }
            Gizmos.DrawLine(LastPos, drawpoint);
            LastPos = drawpoint;
        }
    }

    #endregion
}
