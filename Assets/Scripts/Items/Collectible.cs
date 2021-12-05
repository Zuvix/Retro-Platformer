using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    [SerializeField]
    float distance=0.4f;
    [SerializeField]
    float timeToMove = 0.75f;
    float maxY;
    float minY;
    [SerializeField]
    int scoreValue;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        transform.DOMoveY(transform.position.y + distance/2, timeToMove/2)
            .OnComplete(MoveDown)
            .SetEase(Ease.Linear);
    }
    public void MoveUp()
    {
        transform.DOMoveY(transform.position.y + distance, timeToMove)
            .OnComplete(MoveDown)
            .SetEase(Ease.Linear);
    }
    public void MoveDown()
    {
        transform.DOMoveY(transform.position.y - distance, timeToMove)
            .OnComplete(MoveUp)
            .SetEase(Ease.Linear);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (scoreValue > 0)
            {
                ScoreManager.Instance.AddScore(scoreValue, transform.position);
            }
            Destroy(this.gameObject);

        }
    }

}
