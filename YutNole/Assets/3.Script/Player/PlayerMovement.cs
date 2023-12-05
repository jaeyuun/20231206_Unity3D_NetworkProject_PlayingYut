using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private Transform currentPos;
    [SerializeField] private Transform targetPos;
    private Animator playerAni;

    public float speed = 0;

    private void Awake()
    {
        TryGetComponent(out playerAni);
        TryGetComponent(out playerPos);
        PlayerStart();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerStart()
    {
        // Player�� �ǿ� ���� �� ������ ����, �� ������ UI���� �ؾ��� ��
        playerPos.position = currentPos.position;
    }

    private void PlayerMove()
    {
        // Player�� �ǿ� ���� �� ������ ����
        playerPos.position = Vector3.MoveTowards(playerPos.position, targetPos.position, Time.deltaTime * speed);
    }

    private void PlayerCatch()
    {
        // Player�� �ٸ� Player ��ġ�� �������� ��
    }

    private void PlayerFinish()
    {
        // Player�� ���� ��
        gameObject.SetActive(false);
    }
}
