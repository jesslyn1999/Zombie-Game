using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour, IPooledObject
{
    public NinjagirlMovement player;

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 5;

    const int PLAYER_LAYER = 8;
    const int ENEMY_LAYER = 9;

    public GameObject scoreTextObj;
    private Text scoreTextBar;

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
                    int score = enemy.TakeDamage(damage);
                    if (player != null)
                    {
                        player.addScore(score);
                    }
                }
            }
        } else
        {
            player = collision.gameObject.GetComponent<NinjagirlMovement>();
        }

    }

    void IPooledObject.OnObjectSpawn()
    {
        rb.velocity = transform.right * speed;
    }
}
