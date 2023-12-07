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
        Time.timeScale = 3;
        playingYut = FindObjectOfType<PlayingYut>();
        // UI Player �������� ���� ���߿� �̵�
        playerArray = playingYut.pos1;
        playingYut.playerArray = playerArray;
    }

    public void PlayerMove()
    {
        transform.position = playerArray[currentIndex].position; // �÷��̾� ���� ������
        targetIndex = playingYut.currentIndex; // ��ư�� ������ �� �̵��� �÷��̾� Ÿ�� �ε���
        if (targetIndex >= playerArray.Length) // ���� �ε����� target Index���� ������ Backdo �ƴϸ� �⺻
        { // Goal
            targetIndex = playerArray.Length - 1;
        }
        targetPos = playerArray[targetIndex];

        StartCoroutine(Move_Co());

        playerArray = playingYut.playerArray;
        currentIndex = targetIndex;
        if (targetIndex >= playerArray.Length)
        {
            playingYut.GoalButtonClick();
        }
    }

    private IEnumerator Move_Co()
    {
        if (currentIndex > targetIndex)
        { // Backdo
            targetIndex = currentIndex - targetIndex;
            Debug.Log("Backdoindex: " + targetIndex);
        }

        for (int i = currentIndex; i <= targetIndex; i++)
        {
            targetPos = playerArray[i];
            while (transform.position != targetPos.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * speed);
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
        }

        if (playingYut.yutGacha.isChance)
        {
            playingYut.playerButton[0].SetActive(true);
        }
    }
}
