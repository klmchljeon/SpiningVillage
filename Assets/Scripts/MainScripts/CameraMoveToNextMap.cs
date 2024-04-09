using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMoveToNextMap : MonoBehaviour
{
    // ī�޶� �̵� �ӵ�
    public float smoothSpeed = 0.125f;
    // �� ��ġ �迭
    public Transform[] mapPositions;
    // ���� �� �ε���
    private int currentMapIndex = 0;

    void FixedUpdate()
    {
        // ���� ī�޶� ��ġ
        Vector3 startPos = Camera.main.transform.position;
        // ��ǥ ī�޶� ��ġ
        Vector3 endPos = new Vector3(mapPositions[currentMapIndex].position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        // ī�޶� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(startPos, endPos, smoothSpeed);
        Camera.main.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
    }

    // ���� ������ �̵�
    public void MoveToNextMap()
    {
        currentMapIndex = (currentMapIndex + 1) % mapPositions.Length;
    }

    // ���� ������ �̵�
    public void MoveToPreviousMap()
    {
        if (currentMapIndex == 0)
        {
            currentMapIndex = mapPositions.Length - 1;
        }
        else
        {
            currentMapIndex--;
        }
    }

    public void OnChildCollisionEnter(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� "Player" �±װ� ������ ������Ʈ�� ���Դ��� Ȯ��
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            if (other.transform.position.x > cameraPosition.x)
            {
                MoveToNextMap();
            }
            else
            {
                MoveToPreviousMap();
            }


        }
    }
}
