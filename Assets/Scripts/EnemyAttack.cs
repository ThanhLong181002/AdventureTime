using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDistance = 1f;
    public int attackDamage = 10;
    public float attackDelay = 3f;

    private GameObject player;
    private HealthPlayer playerHealth;
    public bool canAttack = true;
    public GameObject AttackRange;

    private Animator animator;

    void Start()
    {
        AttackRange.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<HealthPlayer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Tính khoảng cách giữa Enemy và Player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackDistance && canAttack)
        {
            // Nếu khoảng cách đủ gần và Enemy có thể tấn công, tấn công Player
            // playerHealth.TakeDamage(attackDamage);

            // Kích hoạt animation Attack
            animator.SetTrigger("EAttack");
            animator.SetBool("isAttack", true);
            AttackRange.gameObject.SetActive(true);
            

            // Trì hoãn tấn công Enemy trong 3 giây, sử dụng coroutine
            StartCoroutine(WaitForAttackDelay());
            Invoke("ResetState", 1f);
        }
        
    }
    void ResetState()
    {
        animator.ResetTrigger("EAttack");
        animator.SetBool("isAttack", false);
        AttackRange.gameObject.SetActive(false);
    }

    IEnumerator WaitForAttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

}
