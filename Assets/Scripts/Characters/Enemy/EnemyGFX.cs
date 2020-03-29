using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour, IEnemy, IPooledObject
{
    public int health = 5;
    public int damage = 1;

    public AIPath aIPath;

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

    void IEnemy.TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PLAYER_LAYER)
        {
            Debug.Log("collide");
            NinjagirlMovement player = collision.GetComponent<NinjagirlMovement>();
            if (player != null)
            {
                Debug.Log("take rest");
                player.TakeDamage(damage);
            }
        }

    }

    void IPooledObject.OnObjectSpawn()
    {
        transform.parent.gameObject.SetActive(true);
    }
}
