using UnityEngine;

public class Background : MonoBehaviour
{
    public float bgSpeed = 1f;
    private float bgWidth;
    private float startPos;

    private void Start()
    {
        // 배경의 너비 구하기
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            bgWidth = spriteRenderer.bounds.size.x;
        }

        // 시작 위치 설정
        startPos = transform.position.x;
    }

    private void Update()
    {
        // 배경을 왼쪽으로 이동
        transform.Translate(Vector3.left * bgSpeed * Time.deltaTime);

        // 배경이 왼쪽으로 움직이며 화면을 벗어나면 시작 위치로 되돌림
        if (transform.position.x < startPos - bgWidth)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
