using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float attackDistance = 5f;
    public int attackDamage = 10;
    public float attackDelay = 3f;

    private GameObject player;
    private HealthPlayer playerHealth;
    public bool canAttack = true;
    public GameObject AttackRange;

    private Animator animator;

    public GameObject ball;
    public Transform ballPos;

    private float timer;

    void Start()
    {
        AttackRange.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<HealthPlayer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x); // Tính khoảng cách theo trục X giữa Enemy và Player
        float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y); // Tính khoảng cách theo trục Y giữa Enemy và Player
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            Ban();
            timer = 0f;
        }    
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

    void Ban()
    {
        Vector2 direction = player.transform.position - transform.position; // Tính hướng bắn đạn
        GameObject bullet = Instantiate(ball, ballPos.position, Quaternion.identity); // Tạo đạn
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * 5f; // Thiết lập vận tốc cho đạn
    }
}
