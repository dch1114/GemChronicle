using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 velocity;
    public bool isGrounded { get; } = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
    }

    public void Move(Vector3 _speed)
    {
        //need Camera Limit
        transform.Translate(_speed);
    }

    public void Look(bool isLeft)
    {
        transform.localScale = isLeft ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
    }

    public void Jump(float _jumpForce)
    {
        rb.velocity += new Vector2(0f, _jumpForce);
    }
}
