using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int diem = 100;
    private bool isHurt = false;
    public int maxHealth = 100;
    public int currentHealth;

    private Animator animator;
    private Rigidbody2D rb;

    public float hurtTime = 2f;
    public float deathDelay = 2f; // Thời gian trì hoãn trước khi xóa Enemy khỏi Scene

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animator.SetBool("isHurt", isHurt);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackbyPlayer"))
        {
            if (!isHurt)
            {
                currentHealth -= 25;
                if (currentHealth <= 0)
                {
                    Die();
                }
                animator.SetTrigger("Hurt");
                isHurt = true;
                StartCoroutine(DisableHurt());
            }
        }
    }

    void Die()
    {
        animator.SetTrigger("Dead"); // Kích hoạt animation clip "Dead"
        Finn.Score += diem;
        StartCoroutine(DestroyAfterDelay(deathDelay));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void ResetState()
    {
        animator.ResetTrigger("Hurt");
    }

    IEnumerator DisableHurt()
    {
        yield return new WaitForSeconds(hurtTime);
        isHurt = false;
        ResetState();
    }
}
