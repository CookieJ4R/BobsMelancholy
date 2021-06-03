using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{

    public bool canMove = true;
    public bool prohibitAnyMovement = false;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .05f;
    public float accelerationTimeGrounded = 0f;
    public float moveSpeed = 6;

    public Vector2 wallLeap;
    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;


    bool isDashing = false;

    public bool canWallJump = true;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    PlayerAnimation playerAnimation;
    AudioManager am;
    LevelStatsManager lsm;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        lsm = GameObject.Find("Manager").GetComponent<LevelStatsManager>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

    }

    void Update()
    {
        if (canMove)
        {
            CalculateVelocity();
            HandleWallSliding();
            controller.Move(velocity * Time.deltaTime, directionalInput);
        }
        else
            controller.Move(Vector2.zero, Vector2.zero);

        if (controller.collisions.above || controller.collisions.below)
            velocity.y = 0;

            if (velocity.y < 0)
                velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime;
            else if (velocity.y > 0 && (!Input.GetButton("Jump") || isDashing))
                velocity.y += gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
 
        
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (canMove)
        {
            if (wallSliding)
            {
                am.PlayWallJumpSound();
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
                controller.collisions.jumpCount = 1;
            }
            else if (controller.collisions.below || controller.collisions.jumpCount < 2)
            {
                am.PlayJumpSound();
                velocity.y = maxJumpVelocity;
                controller.collisions.jumpCount++;
            }
        }
    }

    public void Dash()
    {
        if (canMove)
        {
            if (!controller.collisions.dashed)
            {
                if (directionalInput.y != 0 || directionalInput.x != 0 || !controller.collisions.below)
                    playerAnimation.playDashAnimation();
                isDashing = true;
                velocity = Vector3.zero;
                controller.Move(velocity, false);
                Vector3 direction = new Vector3(directionalInput.x, directionalInput.y).normalized;
                Vector3 speed = new Vector3(30, 15 + (10 * Mathf.Abs(direction.x)));
                velocity += Vector3.Scale(direction, speed);
                controller.collisions.dashed = true;
                Invoke("ResetDash", 0.125f);
            }
        }
    }

    void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;

        wallSliding = false;

        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                    timeToWallUnstick -= Time.deltaTime;
                else
                    timeToWallUnstick = wallStickTime;
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        if(!isDashing)
            velocity.y += gravity * Time.deltaTime;
    }

    void ResetDash()
    {
        isDashing = false;
    }

    public void Die()
    {
        lsm.AddLevelDeath();
        playerAnimation.playDeathAnimation();     
    }

    public void ToggleCanMoveNextFrame()
    {
        StartCoroutine(ToggleCanMove());
    }

    IEnumerator ToggleCanMove()
    {
        yield return null;
        if(!prohibitAnyMovement)
            canMove = !canMove;
    }

}
