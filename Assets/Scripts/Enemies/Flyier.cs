using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Flyier : Enemy
{
    [SerializeField]
    float length;
    [SerializeField]
    float timeToMove;
    [SerializeField]
    float floatTime;
    int waitTillFlying = 0;
    [SerializeField]
    bool startDirectionIsUp = true;
    // Start is called before the first frame update
    private void Start()
    {
        if (startDirectionIsUp)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }

    }
    public void MoveUp()
    {
        //Debug.Log("Moving up");
        waitTillFlying = 3;
        animator.speed = 0.85f;
        transform.DOMoveY(transform.position.y + length, timeToMove)
            .OnComplete(FloatDown)
            .SetEase(Ease.Linear);
    }
    public void FloatUp()
    {
        //Debug.Log("Floating up");
        animator.speed = 0.0f;
        animator.Play("Flying", 0, 0f);
        waitTillFlying--;
        if (waitTillFlying < 0)
        {
            MoveDown();
        }
        else
        {
            transform.DOMoveY(transform.position.y + 0.25f, floatTime / 3)
                .OnComplete(FloatDown)
                .SetEase(Ease.Linear);
        }
    }
    public void FloatDown()
    {
        //Debug.Log("Floating down");
        waitTillFlying--;
        animator.speed = 0.0f;
        animator.Play("Flying", 0, 0.5f);
        if (waitTillFlying < 0)
        {
            MoveUp();
        }
        else
        {
            transform.DOMoveY(transform.position.y - 0.25f, floatTime/3)
                .OnComplete(FloatUp)
                .SetEase(Ease.Linear);
        }
    }
    public void MoveDown()
    {
        //Debug.Log("Moving down");
        waitTillFlying = 3;
        animator.speed = 0.85f;
        transform.DOMoveY(transform.position.y - length, timeToMove)
            .OnComplete(FloatUp)
            .SetEase(Ease.Linear);
    }
    public override void Die()
    {
        base.Die();
        DOTween.Kill(transform);
    }

}
