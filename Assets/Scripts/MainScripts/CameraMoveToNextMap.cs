using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMoveToNextMap : MonoBehaviour
{
    // 카메라 이동 속도
    public float smoothSpeed = 0.5f;
    // 맵 위치 배열
    public Transform[] mapPositions;
    // 현재 맵 인덱스
    private int currentMapIndex = 0;
    // 이동 여부
    private bool isMoved;
    // 플레이어 
    private GameObject player;

    private Vector3 lastPosition;
    private float lastSpeed = 0f;
    private bool isSpeedDecreasing = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        lastPosition = Camera.main.transform.position;
    }

    void FixedUpdate()
    {
        currentMapIndex = FindMapIdx();
        isMoved = MoveCamera(currentMapIndex);

        CheckDecreasing();
        if (isSpeedDecreasing && !isMoved)
        {
            TeleportOriginalMap(currentMapIndex);
        }
    }

    private void CheckDecreasing()
    {
        // 현재 프레임과 이전 프레임 사이의 거리 차이를 시간으로 나누어 현재 속도를 계산
        float currentSpeed = (Camera.main.transform.position - lastPosition).magnitude / Time.deltaTime;

        // 이전 속도와 비교하여 속도가 감소했는지 확인
        isSpeedDecreasing = currentSpeed < lastSpeed;

        // 이번 프레임의 위치와 속도를 '이전' 값으로 업데이트
        lastPosition = Camera.main.transform.position;
        lastSpeed = currentSpeed;
    }

    private void TeleportOriginalMap(int idx)
    {
        int i;
        if (idx == 0) i = 4;
        else if (idx == 5) i = 1;
        else return;

        Vector3 playerPosition = player.transform.position;
        Vector3 offset = playerPosition - Camera.main.transform.position;

        Camera.main.transform.position = new Vector3(mapPositions[i].position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        player.transform.position = Camera.main.transform.position + offset;
        return;
    }

    private bool MoveCamera(int idx)
    {
        bool res = false;

        // 현재 카메라 위치
        Vector3 startPos = Camera.main.transform.position;
        // 목표 카메라 위치
        Vector3 endPos = new Vector3(mapPositions[idx].position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        res |= Vector3.Distance(startPos,endPos) > 0.01f;

        // 카메라 이동
        Vector3 smoothedPosition = Vector3.Lerp(startPos, endPos, smoothSpeed);
        Camera.main.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
        return res;
    } 

    private int FindMapIdx()
    {
        Vector3 playerPosition = player.transform.position;
        int idx = -1;
        float dis = 100;
        for (int i = 0; i < mapPositions.Length; i++)
        {
            float tmp = Mathf.Abs(mapPositions[i].position.x - playerPosition.x);
            if (dis > tmp)
            {
                dis = tmp;
                idx = i;
            }
        }
        return idx;
    }
}
