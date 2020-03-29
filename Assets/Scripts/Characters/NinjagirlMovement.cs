using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjagirlMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    public int health = 50;

    const string IS_IDLE = "isIdle";
    const string IS_DEAD = "isDead";
    const string IS_JUMPING = "isJumping";
    const string IS_JUMPING_DOWN = "isJumpingDown";
    const string IS_RUNNING = "isRunning";
    const string IS_ATTACKING = "isAttacking";
    const string IS_THROWING = "isThrowing";
    const string IS_SLIDING = "isSliding";

    const float INIT_SPEED = 25f;


    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void FixedUpdate()
    {
        // Move character
        controller.Move(Time.fixedDeltaTime * horizontalMove, crouch, jump);
        jump = false;  // Not jump forever
    }

    void movement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && controller.isGrounded())
        {
            stopAllAnim();
            jump = true;
            setMovementTrue(IS_JUMPING);
            runSpeed = 0.7f * INIT_SPEED;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            if (!isMovementTrue(IS_SLIDING))
                stopAllAnim();
            setMovementTrue(IS_SLIDING);
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Debug.Log("is up sliding");
            setMovementFalse(IS_SLIDING);
            crouch = false;
        }

        if (horizontalMove != 0 && isMovementTrue(IS_IDLE) && !isMovementTrue(IS_RUNNING) && !isMovementTrue(IS_SLIDING))
        {
            stopAllAnim();
            setMovementTrue(IS_RUNNING);
        } else if (horizontalMove==0)
        {
            setMovementFalse(IS_RUNNING);
        }

        if (!isAnyAnimActive())
        {
            setMovementTrue(IS_IDLE);
        }
    }

    public void isGrounded()
    {
        if (isMovementTrue(IS_JUMPING) && isMovementTrue(IS_JUMPING_DOWN))
        {
            Debug.Log("arrive");
            stopAllAnim();
            runSpeed = INIT_SPEED;
        } else if (isMovementTrue(IS_JUMPING))
        {
            setMovementTrue(IS_JUMPING_DOWN);
        }
        
    }

    public void onSliding(bool isSLiding)
    {
        animator.SetBool(IS_SLIDING, isSLiding);
    }

    void stopAllAnim()
    {
        setMovementFalse(IS_IDLE);
        setMovementFalse(IS_RUNNING);
        setMovementFalse(IS_DEAD);
        setMovementFalse(IS_JUMPING);
        setMovementFalse(IS_THROWING);
        setMovementFalse(IS_SLIDING);
        setMovementFalse(IS_ATTACKING);
        setMovementFalse(IS_JUMPING_DOWN);
    }

    bool isAnyAnimActive()
    {
        return isMovementTrue(IS_IDLE)|| isMovementTrue(IS_RUNNING)|| isMovementTrue(IS_DEAD)
            || isMovementTrue(IS_JUMPING)|| isMovementTrue(IS_THROWING)|| isMovementTrue(IS_SLIDING)
            || isMovementTrue(IS_ATTACKING) || isMovementTrue(IS_JUMPING_DOWN);
    }

    void setMovementTrue(string movement)
    {
        animator.SetBool(movement, true);
    }

    void setMovementFalse(string movement)
    {
        animator.SetBool(movement, false);
    }

    bool isMovementTrue(string movement)
    {
        return animator.GetBool(movement);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        setMovementTrue(IS_DEAD);
        gameObject.SetActive(false);
    }
}
