using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoverType
{
    Wallie,
    Timmie,
}
public class Mover : MovingEnemy
{
    [SerializeField]
    private MoverType moverType;
    [SerializeField]
    private float speed;
    float timer;
    [SerializeField]
    float maxTimer;

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
        if(IsCloseToWall()|| IsCloseToFall())
        {
            Flip();
        }
    }
}
