using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMoveToNextMap : MonoBehaviour
{
    // ī�޶� �̵� �ӵ�
    public float smoothSpeed = 0.5f;
    // �� ��ġ �迭
    public Transform[] mapPositions;
    // ���� �� �ε���
    private int currentMapIndex = 0;
    // �̵� ����
    private bool isMoved;
    // �÷��̾� 
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
        // ���� �����Ӱ� ���� ������ ������ �Ÿ� ���̸� �ð����� ������ ���� �ӵ��� ���
        float currentSpeed = (Camera.main.transform.position - lastPosition).magnitude / Time.deltaTime;

        // ���� �ӵ��� ���Ͽ� �ӵ��� �����ߴ��� Ȯ��
        isSpeedDecreasing = currentSpeed < lastSpeed;

        // �̹� �������� ��ġ�� �ӵ��� '����' ������ ������Ʈ
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

        // ���� ī�޶� ��ġ
        Vector3 startPos = Camera.main.transform.position;
        // ��ǥ ī�޶� ��ġ
        Vector3 endPos = new Vector3(mapPositions[idx].position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        res |= Vector3.Distance(startPos,endPos) > 0.01f;

        // ī�޶� �̵�
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
