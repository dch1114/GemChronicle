using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 currentVelocity = new Vector3(0, 0, 0);
    private Vector3 impact;

    public Vector2 velocity;
    public bool isGrounded { get; set; } = false;


    public Vector3 Movement => impact + Vector3.up * velocity.y;

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector3.down, Color.green);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (velocity.y <= 0f && isGrounded)
        {
            velocity = Vector2.zero;
        }
        else
        {
            velocity += (Physics2D.gravity) * Time.fixedDeltaTime;
            transform.position += new Vector3(velocity.x, velocity.y);
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref currentVelocity, 0.3f);
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
        velocity.y += _jumpForce;
    }
}
