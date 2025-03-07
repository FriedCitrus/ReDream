using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables - dash
    [SerializeField]private float dashPower = 15f;
    [SerializeField]private float dashDuration = 0.5f;
    [SerializeField]private bool isDashing = false;
    [SerializeField]private bool canDash = true;
    [SerializeField]private int dashCounter = 2;

    [SerializeField]private bool AirDrag = false;

    //variables - jump
    [SerializeField]private float jumpPower = 25f;
    [SerializeField]private bool canJump = true;
    [SerializeField]private float jumpCooldown = 5f;
    private float jumpCooldownTimer;

    //references
    public Rigidbody2D rb;
    public InputReader inputReader; //reference to the InputReader script
    public Transform groundCheck;
    public LayerMask groundLayer;

    //airtime can be kinda useful for some mechanics or achievements or whatever
    private float airTime = 0f;

    // Public properties to access private variables
    public float AirTime => airTime;
    public int DashCounter => dashCounter;


    // Update is called once per frame
    void Update()
    {

        switch(inputReader.state){
            //jump
            case "Up":
            airTime = 0f;
                if(canJump && isGrounded()){
                    rb.velocity = new Vector2(0, jumpPower);
                    canJump = false;
                    jumpCooldownTimer = Time.time;
                }
                break;
            //dash right
            case "Right":
                if (!isDashing && dashCounter > 0)
                {
                    StartCoroutine(Dash(Vector2.right));
                    dashCounter--;
                }
                break;
            //dash left
            case "Left":
                if (!isDashing )
                {
                    StartCoroutine(Dash(Vector2.left));
                    dashCounter--;
                }
                break;
            //stomp - working only when in air (kinda finicky to pull it off for some reason)
            case "Down":
                if(!isGrounded())
                {
                    rb.velocity = new Vector2(0, -3*jumpPower);
                }
                break;
            //why is this here?
            case "None":
                break;
            default:
                break;
        }
        if(Time.time - jumpCooldownTimer > jumpCooldown)
        {
            canJump = true;
        }

        //count airtime
        if(!isGrounded()){
            airTime += Time.deltaTime;
        }else{
            dashCounter = 2;
            if(AirDrag){
                rb.velocity = Vector2.zero;
                AirDrag = false;
            }
        }
    }

    //dash function
    private IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        rb.velocity = direction * dashPower;
        yield return new WaitForSeconds(dashDuration);
        AirDrag = true;
        rb.velocity = direction * dashPower * 0.25f;
        isDashing = false;
    }

    private void FixedUpdate()
    {
        //smart code goes here
    }

    //check the button press (I didn't figure it out lmao)
    private void checkButtonPress()
    {
        Debug.Log("The button is pressed");
    }

    //check if player is grounded
    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
