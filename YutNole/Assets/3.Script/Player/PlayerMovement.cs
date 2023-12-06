using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayingYut playingYut;

    public Transform startPos; // �ӽ� start

    public Transform[] playerArray; // player�� �ش��ϴ� pos array
    public int currentIndex = 0; // player ���� index
    public int targetIndex = 0; // ��ư Ŭ�� �� �̵��� index
    public Transform targetPos; // ��ư Ŭ�� �� �̵��� ��ġ

    public float speed = 2f;

    private void Awake()
    {
        playingYut = FindObjectOfType<PlayingYut>();
        // UI Player �������� ���� ���߿� �̵�
        playerArray = playingYut.pos1;
        playingYut.playerArray = playerArray;
    }

    public void PlayerMove()
    {
        transform.position = playerArray[currentIndex].position;
        targetIndex = playingYut.currentIndex + 1;
        targetPos = playerArray[targetIndex];
        StartCoroutine(Move_Co());
        currentIndex += targetIndex;

        // ��ư ���� ��
        switch (currentIndex)
        { // ���� ��ġ Ȯ�� �� �迭 �ٲ��ֱ�
            case 5:
            case 10:
            case 22:
                playingYut.TurnPosition(playerArray, currentIndex);
                playerArray = playingYut.playerArray;
                playingYut.currentIndex = this.currentIndex;
                break;
        }
    }

    private IEnumerator Move_Co()
    {
        for (int i = currentIndex; i < targetIndex; i++)
        {
            targetPos = playerArray[i];
            while (transform.position != targetPos.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * speed);
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
