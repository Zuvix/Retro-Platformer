using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Combat : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Movement movement;
    Animator animator;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }
   void KillPlayer()
    {
        movement.enabled = false;
        rigidBody.velocity = Vector2.zero;
        animator.SetBool("isDead", true);
        this.enabled = false;
        SceneLevelManager.Instance.PlayerDiedReload();
    }
    void FallKillPlayer()
    {
        movement.enabled = false;
        this.enabled = false;
        SceneLevelManager.Instance.PlayerDroppedReload();
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
                KillPlayer();
            }
        }
        if (collision.CompareTag("Void"))
        {
            FallKillPlayer();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            ITrap trap = collision.gameObject.GetComponent<ITrap>();
            if (trap.IsActive())
            {
                KillPlayer();
            }
        }
    }
}
