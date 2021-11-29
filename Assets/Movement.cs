using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;
    BoxCollider2D boxCollider2d;
    Animator animator;
    SpriteRenderer sr;
    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;
    public float jumpVelocity = 5f;
    public float fallingModifier = 0.05f;
    bool hasJumped = false;
    public LayerMask platformLayerMask;

    //Used to jump higher if button is hold longer
    bool jumpingBtnHeld = false;
    float jumpingTimer;
    bool isGrounded = false;


    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        HandleJumpInput();
        isGrounded = IsGrounded();
        animator.SetBool("isGrounded", isGrounded);
        if (horizontal == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        if (horizontal >= 0)
        {
            sr.flipX = false;
        }
        else 
        {
            sr.flipX = true;

        }

    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed,body.velocity.y);
        Fall();
        Jump();
        JumpAddition();
        animator.SetFloat("flySpeed", body.velocity.y);
    }
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpingBtnHeld = true;
            hasJumped = true;
        }
        if (jumpingBtnHeld == true)
        {
            jumpingTimer += Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpingBtnHeld = false;
            jumpingTimer = 0;
        }
    }
    public void Fall()
    {
        if (!isGrounded && body.velocity.y <= 4f)
        {
            body.velocity += new Vector2(0, -fallingModifier);
        }
    }
    public void Jump()
    {
        if (hasJumped)
        {
            Debug.Log("Jumping");
            body.velocity = new Vector2(body.velocity.x, jumpVelocity * 0.5f);
            hasJumped = false;
        }
    }
    public void JumpAddition()
    {
        if (isGrounded)
        {
            return;
        }
        if(jumpingTimer<0.2f && jumpingBtnHeld && body.velocity.y>0)
        {
            body.velocity += new Vector2(0, jumpVelocity*Time.deltaTime*2.5f);
        }
    }
    private bool IsGrounded()
    {
        float extraHeightText = 0.075f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);

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
}

