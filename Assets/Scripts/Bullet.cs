using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private LayerMask m_WhatIsColliders;

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 5;

    const int PLAYER_LAYER = 8;
    const int ENEMY_LAYER = 9;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != PLAYER_LAYER)
        {
            gameObject.SetActive(false);
            if (collision.gameObject.layer == ENEMY_LAYER)
            {
                IEnemy enemy = collision.GetComponent<IEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

    }

    void IPooledObject.OnObjectSpawn()
    {
        rb.velocity = transform.right * speed;
    }
}
