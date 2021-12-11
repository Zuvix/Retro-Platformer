using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    float bouncePower=10f;
    [SerializeField]
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rigidbody= collider.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, bouncePower);
            animator.Play("Trampoline",0,0f);
        }
    }
}
