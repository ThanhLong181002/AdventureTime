using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBall : MonoBehaviour
{
    public int damage = 1; // Số máu mà đạn sẽ gây ra khi va chạm với Player

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Phá hủy đạn

        }
    }
}
