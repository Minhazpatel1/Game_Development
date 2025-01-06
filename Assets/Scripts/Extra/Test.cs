using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector2 Direction = new Vector2(HorizontalInput, VerticalInput).normalized;
        rb.velocity = Direction * speed;
    }
}
