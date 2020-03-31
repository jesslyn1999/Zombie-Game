using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NinjagirlMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public string username = "dummy";
    public float INIT_SPEED = 40f;
    public int INIT_HEALTH = 20;
    public float runSpeed;
    public int health;
    public int score = 0;
    public HealthBar healthBar;
    public TextMeshProUGUI scoreTextBar;
    public GameObject gameOverPanel;
    public AudioSource runningAudio;
    public AudioSource shootingAudio;
    public Transform firePoint;
    public LayerMask enemyLayers;
    public float attackRange = 1.2f;
    public int swordDamage = 10;


    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool wasDied = false;

    const string IS_JUMPING = "isJumping";
    const string IS_RUNNING = "isRunning";
    const string IS_ATTACKING = "isAttacking";
    const string IS_THROWING = "isThrowing";
    const string IS_SLIDING = "isSliding";
    const string IS_DEAD = "isDead";



    // Start is called before the first frame update
    void Start()
    {
        health = INIT_HEALTH;
        runSpeed = INIT_SPEED;
        healthBar.SetHealthMax(INIT_HEALTH);
        setScoreTextBar();
        gameOverPanel.SetActive(false);
        username = PlayerPrefs.GetString("username");
    }
    // Update is called once per frame
    void Update()
    {
        if (health > 0)
            movement();
        else
        {
            if (!wasDied)
                StartCoroutine("Die");
            wasDied = true;
        }
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

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            setMovementTrue(IS_JUMPING);
            runSpeed = 0.6f * INIT_SPEED;
        }

        if (!isMovementTrue(IS_JUMPING))
        {
            runSpeed = INIT_SPEED;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            setMovementTrue(IS_SLIDING);
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            setMovementFalse(IS_SLIDING);
            crouch = false;
        }


        if (horizontalMove != 0) {
            runningAudio.Play();
            setMovementTrue(IS_RUNNING);
        }

        if (horizontalMove == 0) {
            runningAudio.Stop();
            setMovementFalse(IS_RUNNING);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shootingAudio.Play();
            setMovementTrue(IS_THROWING);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            shootingAudio.Stop();
            setMovementFalse(IS_THROWING);
        }

        if (Input.GetButtonDown("Attack"))
        {
            shootingAudio.Play();
            setMovementTrue(IS_ATTACKING);
            SwordAttack();
        }
        else if (Input.GetButtonUp("Attack"))
        {
            shootingAudio.Stop();
            setMovementFalse(IS_ATTACKING);
        }
    }

    private void SwordAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemyCollid in hitEnemies)
        {
            IEnemy enemy = enemyCollid.GetComponent<IEnemy>();
            if (enemy != null)
            {
                int score = enemy.TakeDamage(swordDamage);
                addScore(score);
                Debug.Log("YEY: " + score);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;
        Gizmos.DrawWireSphere(firePoint.position, attackRange);
    }

    public void addScore(int m_score)
    {
        score += m_score;
        setScoreTextBar();
    }

    private void setScoreTextBar()
    {
        scoreTextBar.text = "Score: " + score.ToString();
    }

    public void isGrounded()
    {
       setMovementFalse(IS_JUMPING);
    }

    public void onSliding(bool isSliding)
    {
        Debug.Log("ON SLIDING: " + isSliding);
        animator.SetBool(IS_SLIDING, isSliding);
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
        healthBar.SetHealth(health);
    }

    IEnumerator Die()
    {
        _ = RestApi.PostScore(username, score);
        animator.SetBool(IS_DEAD, true);
        //Destroy(gameObject, 6f);
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(6f);
        gameObject.SetActive(false);
    }
}
