using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoverType
{
    Wallie,
    Timmie,
}
public class Mover : FallingEnemy
{
    [SerializeField]
    private MoverType moverType;
    private IEnumerator movement;
    [SerializeField]
    private bool movingRight;
    [SerializeField]
    private float speed;
    float timer;
    [SerializeField]
    float maxTimer;
    [SerializeField]
    LayerMask wallMask;
    Vector2 wallCheckTarget;
    protected override void Awake()
    {
        base.Awake();
        if (movingRight)
        {
            wallCheckTarget = Vector2.right;
        }
        else
        {
            wallCheckTarget = Vector2.left;
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isGrounded)
        {
            animator.SetBool("isWallking",true);
            CheckDirectionChangeCondition();
            if (movingRight)
            {
                rigidBody.velocity = new Vector3(speed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector3(-speed, rigidBody.velocity.y);
            }

            if (moverType.Equals(MoverType.Timmie))
            {
                timer += Time.fixedDeltaTime;
            }
        }
        else
        {
            animator.SetBool("isWallking", false);
        }
    }

    public void CheckDirectionChangeCondition()
    {
        if (moverType.Equals(MoverType.Timmie))
        {
            if (timer > maxTimer)
            {
                timer -= maxTimer;
                Flip();
                return;
            }
        }
        DetectWall();
        DetectFall();
    }
    public void DetectFall()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics2D.Raycast((Vector2)transform.position + 0.5f*wallCheckTarget, Vector2.down, 1f, wallMask))
        {
            Debug.DrawRay((Vector2)transform.position + 0.5f * wallCheckTarget, Vector2.down * 1f, Color.green);
        }
        else
        {
            Flip();
        }
    }
    public void DetectWall()
    {
        if (Physics2D.Raycast(transform.position, wallCheckTarget, 0.5f, wallMask))
        {
            Debug.DrawRay(transform.position, wallCheckTarget * 0.5f, Color.yellow);
            Flip();
        }
        else
        {
            Debug.DrawRay(transform.position, wallCheckTarget * 0.5f, Color.red);
        }
    }
    public void Flip()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        wallCheckTarget = wallCheckTarget * -1;
    }
}
