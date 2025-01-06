using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_player : MonoBehaviour
{
    Rigidbody2D _rb;
    float _playerspeed = 5f;
    float _inputHorizonal;
    float _inputVertical;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _inputHorizonal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");
        Vector2 Direction = new Vector2(_inputHorizonal, _inputVertical).normalized;
        _rb.velocity = Direction * _playerspeed;
    }
}
