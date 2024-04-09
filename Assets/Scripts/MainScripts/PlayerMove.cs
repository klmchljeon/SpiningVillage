using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        // Rigidbody2D 컴포넌트를 가져옵니다.
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 이동할 방향을 가져옵니다. 예를 들어, 입력을 통해.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 현재 위치에서 이동할 방향으로 벡터를 계산합니다.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // 새 위치를 계산합니다.
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // MovePosition을 사용하여 새 위치로 이동합니다.
        rb.MovePosition(newPosition);
    }
}
