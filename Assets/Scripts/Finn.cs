using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Finn : MonoBehaviour
{
    public static int Score;

    public TextMeshProUGUI textScore;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private float moveHorizontal;
    private float moveVertical;
    bool Phai = false;
    bool Trai = false;
    bool Len = false;
    bool Xuong = false;
    int attackCount = 1;
    bool isAttack = false;


    public GameObject attackTriggerUp;
    public GameObject attackTriggerRight;
    public GameObject attackTriggerDown;
    public GameObject attackTriggerLeft;


    void Start()
    {
        Score = 0;
        attackTriggerUp.gameObject.SetActive(false);
        attackTriggerRight.gameObject.SetActive(false);
        attackTriggerLeft.gameObject.SetActive(false);
        attackTriggerDown.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Attack();
        textScore.text = "SCORE: " + Score.ToString(); // Cập nhật nội dung của Text GameObject
        // Xử lý các trạng thái khác của Player ở đây
    }

    void TanCongTheoHuong()
    {
        if (Len && isAttack) attackTriggerUp.gameObject.SetActive(true); 
        if (Phai && isAttack) attackTriggerRight.gameObject.SetActive(true); 
        if (Xuong && isAttack) attackTriggerDown.gameObject.SetActive(true); 
        if (Trai && isAttack) attackTriggerLeft.gameObject.SetActive(true);

        float waitTime = 1f;
        Invoke("ResetTheoHuong", waitTime);
    }

    void ResetTheoHuong()
    {
        attackTriggerUp.gameObject.SetActive(false);
        attackTriggerRight.gameObject.SetActive(false);
        attackTriggerLeft.gameObject.SetActive(false);
        attackTriggerDown.gameObject.SetActive(false);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttack = true;
            TanCongTheoHuong(); 
            if (attackCount == 1)
            {
                animator.ResetTrigger("Attack3");
                animator.SetTrigger("Attack");
                attackCount = 2;
            }
            else if (attackCount == 2)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack2");
                attackCount = 3;
            }
            else if (attackCount == 3)
            {
                animator.ResetTrigger("Attack2");
                animator.SetTrigger("Attack3");
                attackCount = 1;
            }
        }

    }    

    void ResetDonDanh()
    {
        attackCount = 1;
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        isAttack = false;
        attackTriggerUp.gameObject.SetActive(false);
        attackTriggerRight.gameObject.SetActive(false);
        attackTriggerDown.gameObject.SetActive(false);
        attackTriggerLeft.gameObject.SetActive(false);
    }    

    void AttackFinished_Up()
    {
        animator.SetBool("AttackFinished", true);
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.Play("idle-up");
        animator.SetBool("AttackFinished", false);
    }
    void AttackFinished_Down()
    {
        animator.SetBool("AttackFinished", true);
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.Play("idle-down");
        animator.SetBool("AttackFinished", false);
    }
    void AttackFinished_Right()
    {
        animator.SetBool("AttackFinished", true);
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.Play("idle-right");
        animator.SetBool("AttackFinished", false);
    }
    void AttackFinished_Left()
    {
        animator.SetBool("AttackFinished", true);
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.Play("idle-left");
        animator.SetBool("AttackFinished", false);
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        animator.SetFloat("Horizontal", moveHorizontal);
        animator.SetFloat("Vertical", moveVertical);
        animator.SetFloat("Speed", movement.magnitude);
        animator.SetBool("Len", Len);
        animator.SetBool("Xuong", Xuong);
        animator.SetBool("Trai", Trai);
        animator.SetBool("Phai", Phai);
        rb.velocity = movement * moveSpeed;
    }

    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        isAttack = false;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Phai = true;
            Trai = false;
            Len = false;
            Xuong = false;
            ResetDonDanh();

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Phai = false;
            Trai = true;
            Len = false;
            Xuong = false;
            ResetDonDanh();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            Phai = false;
            Trai = false;
            Len = true;
            Xuong = false;
            ResetDonDanh();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            Phai = false;
            Trai = false;
            Len = false;
            Xuong = true;
            ResetDonDanh();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NextLevel"))
        {
            animator.SetTrigger("Victory"); // Kích hoạt animation clip "Victory"
            Invoke("GoToNextLevel", 3f); // Gọi hàm GoToNextLevel() sau 3 giây
        }
    }

    void GoToNextLevel()
    {
        SceneManager.LoadScene("Level2"); // Chuyển sang Scene mới
    }
}
