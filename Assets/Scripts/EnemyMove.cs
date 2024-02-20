using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1f; // tốc độ di chuyển của Enemy
    private Vector3 targetPosition; // vị trí đích của Enemy
    private bool moveHorizontally = true; // biến để đánh dấu Enemy đang di chuyển theo hướng trái phải hay không
    private bool left = false; // biến để đánh dấu Enemy đang di chuyển sang trái hay không
    private bool right = false; // biến để đánh dấu Enemy đang di chuyển sang phải hay không
    private bool up = false; // biến để đánh dấu Enemy đang di chuyển lên hay không
    private bool down = false; // biến để đánh dấu Enemy đang di chuyển xuống hay không

    private float vanToc = 0f; // biến để lưu trữ vận tốc của Enemy

    private GameObject player;

    private float Horizontal;
    private float Vertical;

    public float duoitheo = 0.1f;

    private Animator animator;

    public float WaitingTime = 3f;


    void Start()
    {
        player = GameObject.FindWithTag("Player"); // tìm đối tượng Player theo tag "Player"
        InvokeRepeating("MoveToRandomPosition", 0f, WaitingTime); // gọi MoveToRandomPosition() sau mỗi 3 giây
        animator = GetComponent<Animator>();
    }

    void MoveToRandomPosition()
    {
        float x, y;
        float distance = Vector3.Distance(player.transform.position, transform.position); // tính khoảng cách giữa Player và Enemy

        // nếu khoảng cách giữa Player và Enemy nhỏ hơn hoặc bằng 4, đặt vị trí đích của Enemy là vị trí của Player
        if (distance <= 3f)
        {
            WaitingTime = duoitheo; speed = 2f;
            x = player.transform.position.x;
            y = player.transform.position.y;
        }
        // nếu khoảng cách giữa Player và Enemy lớn hơn 4, tính toán vị trí đích ngẫu nhiên
        else
        {
            WaitingTime = 3f; speed = 1f;
            x = Random.Range(-5f, 5f); // tính toán vị trí ngẫu nhiên trên trục X
            y = Random.Range(-5f, 5f); // tính toán vị trí ngẫu nhiên trên trục Y
        }

        targetPosition = new Vector3(x, y, 0f); // lưu trữ vị trí đích của Enemy

        // đánh dấu Enemy đang di chuyển theo hướng trái phải
        if (Mathf.Abs(transform.position.x - targetPosition.x) > Mathf.Abs(transform.position.y - targetPosition.y))
        {
            moveHorizontally = true;
            if (transform.position.x > targetPosition.x)
            {
                Horizontal = -1;
                Vertical = 0;
                left = true;
                right = false;
                up = false;
                down = false;
            }
            // đánh dấu Enemy đang di chuyển sang phải
            else
            {
                Horizontal = 1;
                Vertical = 0;
                left = false;
                right = true;
                up = false;
                down = false;
            }
        }
        // đánh dấu Enemy đang di chuyển theo hướng lên xuống
        else
        {
            moveHorizontally = false;
            if (transform.position.y < targetPosition.y)
            {
                Horizontal = 0;
                Vertical = 1;
                up = true;
                down = false;
                left = false;
                right = false;
            }
            // đánh dấu Enemy đang di chuyển xuống
            else
            {
                Horizontal = 0;
                Vertical = -1;
                up = false;
                down = true;
                left = false;
                right = false;
            }
        }
        // đặt giá trị vanToc thành 0 khi Enemy mới bắt đầu di chuyển
        vanToc = 0f;
    }

    void Update()
    {
        // di chuyển Enemy theo hướng trái phải
        if (moveHorizontally)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, 0f), speed * Time.deltaTime);
 
        }
        // di chuyển Enemy theo hướng lên xuống
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetPosition.y, 0f), speed * Time.deltaTime);

        }

        // tính toán vận tốc của Enemy
        vanToc = (transform.position - targetPosition).magnitude; // Time.deltaTime;

        // kiểm tra xem Enemy đã đến vị trí đích hay chưa
        if ((transform.position - targetPosition).magnitude < 1f)
        {
            // đặt giá trị vanToc thành 0 khi Enemy đến vị trí đích
            vanToc = 0f;
        }

    }

    private void FixedUpdate()
    {
        animator.SetBool("Len", up);
        animator.SetBool("Xuong", down);
        animator.SetBool("Trai", left);
        animator.SetBool("Phai", right);
        animator.SetFloat("Horizontal", Horizontal);
        animator.SetFloat("Vertical", Vertical);
        animator.SetFloat("Speed", vanToc);
    }
}
