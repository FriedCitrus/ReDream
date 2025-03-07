using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables
    private float hor;
    private float ver;
    [SerializeField]private float jumpPower = 25f;
    [SerializeField]private bool isFacingRight = true;
    [SerializeField]private bool canJump = true;
    [SerializeField]private float jumpCooldown = 5f;
    private float jumpCooldownTimer;

    //references
    public Rigidbody2D rb;
    public InputReader inputReader; //reference to the InputReader script
    public Transform groundCheck;
    public LayerMask groundLayer;

    //count airtime
    //can be kinda useful for some mechanics or achievements
    private float airTime = 0f;
    private bool isInAir = false;


    // Update is called once per frame
    void Update()
    {
        //jumping
        if(inputReader.state == "Up" && canJump && isGrounded()) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            canJump = false;
            jumpCooldownTimer = Time.time;
            isInAir = true;
        }
        if(Time.time - jumpCooldownTimer > jumpCooldown)
        {
            canJump = true;
        }

        //count airtime
        if(isInAir && isGrounded())
        {
            isInAir = false;
            Debug.Log("Air time: " + airTime);
            airTime = 0f;
        }
        if(!isGrounded())
        {
            airTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        //smart code goes here
    }

    //check if player is grounded
    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
