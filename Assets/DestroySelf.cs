using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float lifetime = 5f; // Thời gian tồn tại của đạn

    void Start()
    {
        Destroy(gameObject, lifetime); // Phá hủy đạn sau lifetime giây
    }
}
