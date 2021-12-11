using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Movement movement;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemyAffected = collision.gameObject.GetComponent<Enemy>();
            Vector2 enemyPos = collision.gameObject.transform.position;
            Vector2 playerPos = transform.position;
            float playerfallSpeed = rigidBody.velocity.y*-1;
            if(enemyPos.y < playerPos.y && playerfallSpeed > 0.2f)
            {
                enemyAffected.TakeDamage(1);
                movement.Bounce();
            }
            else
            {
                Debug.Log("PlayerKilled");
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            ITrap trap = collision.gameObject.GetComponent<ITrap>();
            if (trap.IsActive())
            {
                Debug.Log("PlayerKilled");
            }
        }
    }
}
