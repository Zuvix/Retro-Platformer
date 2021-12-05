using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enviroment : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    int objectsInside = 0;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void Hide() 
    {
        objectsInside++;
        if (objectsInside == 1)
            spriteRenderer.DOFade(0.5f, 0.25f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {         
            Hide();
        }
    }
    public void Show()
    {
        objectsInside--;
        if (objectsInside==0)
            spriteRenderer.DOFade(1f, 0.25f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Show();
        }
    }


}
