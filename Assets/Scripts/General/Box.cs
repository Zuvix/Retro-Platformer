using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Box : MonoBehaviour
{
    protected Vector3 startPosition;
    [SerializeField]
    float upwardsDistance=0.4f;
    [SerializeField]
    float movementDuration=0.15f;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void MoveUp()
    {
        transform.DOMoveY(startPosition.y + upwardsDistance,movementDuration).OnComplete(MoveDown);
    }
    void MoveDown()
    {
        transform.DOMoveY(startPosition.y, movementDuration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TouchPlayer();
        }
    }
    protected virtual void TouchPlayer()
    {
        DOTween.Kill(transform);
        MoveUp();
    }
}
