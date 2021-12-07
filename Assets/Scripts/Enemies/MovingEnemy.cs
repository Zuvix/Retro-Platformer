using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    [SerializeField]
    private LayerMask groundLayerMask;
    [SerializeField]
    protected float gravity=0.05f;
    protected bool isGrounded;
    [SerializeField]
    LayerMask wallMask;
    Vector2 wallCheckTarget;
    [SerializeField]
    protected bool movingRight;
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
    protected virtual void FixedUpdate()
    {
        isGrounded = IsGrounded();
        if (!isGrounded)
        {
            rigidBody.velocity += new Vector2(0, -gravity);
        }
        else
        {
            if (rigidBody.velocity.y < 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            }
        }
    }
    private bool IsGrounded()
    {
        float extraHeightText = 0.075f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, groundLayerMask);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);

        return raycastHit.collider != null;
    }
    protected bool IsCloseToFall()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics2D.Raycast((Vector2)transform.position + 0.5f * wallCheckTarget, Vector2.down, 1f, wallMask))
        {
            Debug.DrawRay((Vector2)transform.position + 0.5f * wallCheckTarget, Vector2.down * 1f, Color.green);
            return false;
        }
        else
        {
            return true;
        }
    }
    protected bool IsCloseToWall()
    {
        if (Physics2D.Raycast(transform.position, wallCheckTarget, 0.5f, wallMask))
        {
            Debug.DrawRay(transform.position, wallCheckTarget * 0.5f, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, wallCheckTarget * 0.5f, Color.red);
            return false;
        }
    }
    protected void Flip()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        wallCheckTarget = wallCheckTarget * -1;
    }
}
