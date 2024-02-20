using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        // Tính toán vị trí mới của camera
        Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        newPosition.x = Mathf.Clamp(newPosition.x, -100f, 100f); // Giới hạn phạm vi di chuyển của camera

        // Di chuyển camera đến vị trí mới
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
    }
}
