using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MovingEnemy
{
    [SerializeField]
    float minMoveTime=1.5f;
    [SerializeField]
    float maxMoveTime=4f;
    bool isMoving;
    [SerializeField]
    float movementSpeed=1f;
    [SerializeField]
    float timeSitting;
    bool isSitting;
    bool isTransformed;
    [SerializeField]
    float transformedSpeedMultiplier = 2f;
    Transform player;
    private void Start()
    {
        StartCoroutine(Sit());
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    IEnumerator Move(float timeToMove)
    {
        float timeMoved;
        timeMoved = 0;
        animator.SetBool("isMoving", true);
        while (timeMoved < timeToMove)
        {
            timeMoved += Time.fixedDeltaTime;
            if (IsCloseToFall() || IsCloseToWall())
            {
                Flip();
            }
            if (movingRight)
            {
                rigidBody.velocity = new Vector3(movementSpeed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector3(-movementSpeed, rigidBody.velocity.y);
            }
            yield return new WaitForFixedUpdate();
        }
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y);
        animator.SetBool("isMoving", false);
        StartCoroutine(Sit());
    }
    IEnumerator Sit()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isSitting", true);
        isSitting = true;
        yield return new WaitForSeconds(timeSitting);
        isSitting = false;
        animator.SetBool("isSitting", false);
        yield return new WaitForSeconds(0.5f);
        float randomTime = Random.Range(minMoveTime, maxMoveTime);
        StartCoroutine(Move(randomTime));
    }
    IEnumerator MoveAndKill()
    {
        while (true)
        {
            if (IsCloseToWall())
            {
                Flip();
            }
            if (movingRight)
            {
                rigidBody.velocity = new Vector3(movementSpeed* transformedSpeedMultiplier, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector3(-movementSpeed* transformedSpeedMultiplier, rigidBody.velocity.y);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    public override void Die()
    {
        if (!isTransformed && !isSitting)
        {
            isTransformed = true;
            StopAllCoroutines();
            rigidBody.velocity = Vector2.zero;
            animator.SetBool("isTransformed", true);
        }
        else if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && isTransformed)
        {
            if (player.position.x <= transform.position.x)
            {
                if (movingRight == false) Flip();
            }
            else
            {
                if (movingRight == true) Flip();
            }
            StartCoroutine(MoveAndKill());
        }
        else if (isTransformed && Mathf.Abs(rigidBody.velocity.x) > 0.1f)
        {
            StopAllCoroutines();
            base.Die();
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Enemy"))
        {
            if (isTransformed && Mathf.Abs(rigidBody.velocity.x) > 0.1f)
            {
                collision.GetComponent<Enemy>().TakeDamage(1);
            }
        }
    }

}
