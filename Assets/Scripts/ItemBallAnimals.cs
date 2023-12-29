using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBallAnimals : MonoBehaviour
{
     private Rigidbody2D rigidbody2D;

    void Start()
    {
        // Kiểm tra xem có Rigidbody2D không.
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D not found on tilePrefab.");
        }
    }

    void OnMouseDown()
    {
        // Khi nhấp chuột, thay đổi Gravity Scale thành 1.
        if (rigidbody2D != null)
        {
            rigidbody2D.gravityScale = 1;
        }
    }
}