using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : Entity
{
    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float xInput;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        Movement();

        FlipController();

    }

    private void Movement()
    {

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
}