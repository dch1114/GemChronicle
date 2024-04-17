using UnityEngine;

public class Background : MonoBehaviour
{
    public float bgSpeed = 1f;
    private float bgWidth;
    private float startPos;

    private void Start()
    {
        // ����� �ʺ� ���ϱ�
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            bgWidth = spriteRenderer.bounds.size.x;
        }

        // ���� ��ġ ����
        startPos = transform.position.x;
    }

    private void Update()
    {
        // ����� �������� �̵�
        transform.Translate(Vector3.left * bgSpeed * Time.deltaTime);

        // ����� �������� �����̸� ȭ���� ����� ���� ��ġ�� �ǵ���
        if (transform.position.x < startPos - bgWidth)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
