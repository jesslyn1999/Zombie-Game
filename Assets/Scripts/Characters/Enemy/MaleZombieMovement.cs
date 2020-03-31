using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleZombieMovement : MonoBehaviour, IEnemy, IPooledObject
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 10f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    public int health = 10;
    public int damage = 3;
    public int moveRight = -1;
    public int turn_count = 0;
    public int max_turn = 6;
    public int value = 3;

    const int PLAYER_LAYER = 8;
    const int OBSTACLE_LAYER = 10;

    const string IS_IDLE = "isIdle";
    const string IS_DEAD = "isDead";
    const string IS_WALKING = "isWalking";
    const string IS_ATTACKING = "isAttacking";

    void OnEnable()
    {
        health = 10;
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
        if (!isAnyAnimActive())
        {
            setMovementTrue(IS_IDLE);
        }

        // only randomize 1 or -1 or 0
        horizontalMove = moveRight * runSpeed;
        //Debug.Log("Zombie walk : " + horizontalMove + " " + moveRight + " " + runSpeed);

        if (horizontalMove != 0 && isMovementTrue(IS_IDLE))
        {
            stopAllAnim();
            setMovementTrue(IS_WALKING);
        }
        else if (horizontalMove == 0)
        {
            setMovementFalse(IS_WALKING);
        }
    }


    int IEnemy.TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            return value;
        }
        return 0;
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == PLAYER_LAYER)
        {
            NinjagirlMovement player = collision.gameObject.GetComponent<NinjagirlMovement>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        else if (collision.gameObject.layer == OBSTACLE_LAYER)
        {
            moveRight = -1 * moveRight;
            turn_count += 1;
            if (turn_count > max_turn)
            {
                Die();
            }
        }
    }


    void IPooledObject.OnObjectSpawn()
    {
        moveRight = (int)transform.right.x;
        if (moveRight < 0) controller.setFacingRight(false);
        else controller.setFacingRight(true);
    }

    void stopAllAnim()
    {
        setMovementFalse(IS_IDLE);
        setMovementFalse(IS_WALKING);
        setMovementFalse(IS_DEAD);
        setMovementFalse(IS_ATTACKING);
    }

    bool isAnyAnimActive()
    {
        return isMovementTrue(IS_IDLE) || isMovementTrue(IS_WALKING) || isMovementTrue(IS_DEAD) ||
        isMovementTrue(IS_ATTACKING);
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



}
