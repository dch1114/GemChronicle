using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 velocity;
    public bool isGrounded { get; set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector3.down, Color.green);
        if(hit.collider != null)
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
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
