using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private ElementType gemType;
    [SerializeField] private int xForce = 5;
    [SerializeField] private int yForce = 15;
    [SerializeField] private int gravity = 25;

    private Vector2 direction;
    private bool isGrounded = true;

    private float maxHeight;
    private float currentHeight;

    private void Start()
    {
        currentHeight = Random.Range(yForce - 1, yForce);
        maxHeight = currentHeight;
        direction = new Vector2(Random.Range(-xForce, xForce), 0);
        Initialize(direction);
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (!isGrounded)
        {
            currentHeight += -gravity * Time.deltaTime;
            transform.position += new Vector3(0, currentHeight, 0) * Time.deltaTime;
            transform.position += (Vector3)direction * Time.deltaTime;

            yield return null;
            CheckGroundHit();
        }
    }

    private void Initialize(Vector2 _direction)
    {
        isGrounded = false;
        maxHeight /= 1.5f;
        direction = _direction;
        currentHeight = maxHeight;
    }

    private void CheckGroundHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            // ∂•ø° ¥Í¿Ω
            isGrounded = true;
            transform.position = hit.point;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Instance.PlayClip(SoundManager.Instance.gainGem); //test
        GameManager.Instance.player.Data.StatusData.GetGems(gemType, 1);
        Destroy(gameObject);
    }
}