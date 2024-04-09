using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        // Rigidbody2D ������Ʈ�� �����ɴϴ�.
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // �̵��� ������ �����ɴϴ�. ���� ���, �Է��� ����.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ���� ��ġ���� �̵��� �������� ���͸� ����մϴ�.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // �� ��ġ�� ����մϴ�.
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // MovePosition�� ����Ͽ� �� ��ġ�� �̵��մϴ�.
        rb.MovePosition(newPosition);
    }
}
