using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private Animator animator;

    public float deathDelay = 2f;

    public bool isHurt = false;

    public float hurtTime = 1f;

    private Rigidbody2D rb;

    public GameManagerScript gameManagerScript;


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
    }


    void ResetState()
    {
        animator.ResetTrigger("Hurt");
        animator.SetBool("isHurt", false);
    }

    void Die()
    {
        animator.SetTrigger("Dead");
        animator.SetBool("isDead", true);
        gameManagerScript.gameOver();
        StartCoroutine(DestroyAfterDelay(deathDelay));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackbyEnemy"))
        {
            if (!isHurt)
            {
                currentHealth -= 10;
                healthBar.SetHealth(currentHealth);
                if (currentHealth <= 0)
                {
                    Die();
                }
                animator.SetTrigger("Hurt");
                animator.SetBool("isHurt", true);
                isHurt = true;
                StartCoroutine(DisableHurt());
            }
        }
        if (other.CompareTag("BallEnemy"))
        {
            if (!isHurt)
            {
                currentHealth -= 10;
                healthBar.SetHealth(currentHealth);
                if (currentHealth <= 0)
                {
                    Die();
                }
                animator.SetTrigger("Hurt");
                animator.SetBool("isHurt", true);
                isHurt = true;
                StartCoroutine(DisableHurt());
            }
        }

    }

    void TakeDamage()
    {
        if (!isHurt)
        {
            currentHealth -= 10;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            animator.SetTrigger("Hurt");
            animator.SetBool("isHurt", true);
            isHurt = true;
            StartCoroutine(DisableHurt());
        }
    }    

    IEnumerator DisableHurt()
    {
        yield return new WaitForSeconds(hurtTime);
        isHurt = false;
        ResetState();
    }
}
