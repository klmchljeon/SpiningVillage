using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMoveToNextMap : MonoBehaviour
{
    // 카메라 이동 속도
    public float smoothSpeed = 0.125f;
    // 맵 위치 배열
    public Transform[] mapPositions;
    // 현재 맵 인덱스
    private int currentMapIndex = 0;

    void FixedUpdate()
    {
        // 현재 카메라 위치
        Vector3 startPos = Camera.main.transform.position;
        // 목표 카메라 위치
        Vector3 endPos = new Vector3(mapPositions[currentMapIndex].position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        // 카메라 이동
        Vector3 smoothedPosition = Vector3.Lerp(startPos, endPos, smoothSpeed);
        Camera.main.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
    }

    // 다음 맵으로 이동
    public void MoveToNextMap()
    {
        currentMapIndex = (currentMapIndex + 1) % mapPositions.Length;
    }

    // 이전 맵으로 이동
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
        if (other.CompareTag("Player")) // 트리거에 "Player" 태그가 부착된 오브젝트가 들어왔는지 확인
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
