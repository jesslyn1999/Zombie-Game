using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour, IEnemy, IPooledObject
{
    public int health = 5;
    public int damage = 1;
    public int value = 7;

    public AIPath aIPath;
    public AIDestinationSetter destinationSetter;

    const int PLAYER_LAYER = 8;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-4.5f, 4.5f, 4.5f);
        } else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
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

    void OnEnable()
    {
        health = 5;
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

    }

    void IPooledObject.OnObjectSpawn()
    {
        destinationSetter.target = GameObject.FindWithTag("Player").transform;
    }
}
