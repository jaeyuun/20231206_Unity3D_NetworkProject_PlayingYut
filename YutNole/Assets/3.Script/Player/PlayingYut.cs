using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingYut : MonoBehaviour
{
    /*
     * Yut�� ���� �÷��̾� �̵� �� ��ư �̵�, ȭ��ǥ ���� �� ȭ��ǥ ������ �� �÷��̾� ����
        Trun�� position�� �ɸ��� Transform �迭�� �ٲ��ֱ�
     */
    public Transform[] pos1;
    public Transform[] pos2;
    public Transform[] pos3;
    public Transform[] pos4;

    public Transform[] playerArray; // player�� �ش��ϴ� pos array
    public RectTransform[] yutButton; // ��, ��, ��, ��, ��, ���� ��ư

    // UI���� ��ư ������ �� �޶��� ����
    public PlayerState[] player = new PlayerState[4]; // �ڽ��� ��
    // ĳ���͸��� �پ��ִ� ��ư
    public GameObject[] characterButton; // character
    public GameObject[] returnButton; // return

    public int currentIndex = 0; // Button ��ġ ��ų ���� �ε���, player �����ǰ� �����ؾ� ��
    public int resultIndex = 0; // ��ư ��ġ�� �ε���
    public List<int> yutResultIndex = new List<int>(); // ��ư�� �̵��� ��ġ�� ����

    public int[] yutArray = { 1, 2, 3, 4, 5, -1 }; // �� �� �� �� �� ����

    // �� ��� ��������
    public string yutResult;
    public GameObject goalButton; // goal button, resultIndex���� Ŭ �� SetActive(true)

    private void Start()
    {
        player = GameManager.instance.players;
    }

    public void SetButtons()
    {
        if (GameManager.instance.players[0] != null)
        {
            for (int i = 0; i < characterButton.Length; i++)
            {
                characterButton[i].GetComponent<ButtonPositionSetter>().target = GameManager.instance.players[i].gameObject.transform;
                returnButton[i].GetComponent<ButtonPositionSetter>().target = GameManager.instance.players[i].gameObject.transform;
            }
        }
    }

    public void PlayingYutPlus()
    { // �� ������ ��ư event
        if (!yutResult.Equals("Nack") && !(yutResult.Equals("Backdo") && currentIndex == 0))
        { // ���̰ų� ���� �ε����� 0�̸鼭 ������ ��� ������ ���� ����
            for (int i = 0; i < 4; i++)
            {
                characterButton[i].SetActive(true); // �÷��̾� ���� ��ư, ������ �÷��̾� ������Ʈ�� ��ư�� Ȱ��ȭ X
            }
        }
        // Nack�� ��, �ε��� 0�� �� ������ ��
    }

    public void YutButtonClick(int name)
    { // Canvas - YutObject - ��, ��, ��, ��, ��, ���� event
        for (int i = 0; i < 4; i++)
        {
            characterButton[i].SetActive(false);
            returnButton[i].SetActive(false);
        }

        currentIndex += yutArray[name]; // ���� �ε��� ����Ʈ ������ ���� ������ ����
        yutResultIndex.Remove(name); // �߰��� ����Ʈ ����

        TurnPosition(playerArray, currentIndex); // ���� ��ġ �迭 ����

        PositionOut();
        if (goalButton.activeSelf)
        {
            goalButton.SetActive(false);
        }
    }
    #region Goal Button
    public void GoalButtonClick()
    { // Goal Button Event ... Error ����
        // Goal Count++
        resultIndex = playerArray.Length - 1;
        PositionOut();

        for (int i = 0; i < 4; i++)
        {
            characterButton[i].SetActive(false);
            returnButton[i].SetActive(false);
        }

        /*if (Vector3.Distance(player.transform.position, playerArray[resultIndex - 1].position) <= 0.01f)
        { // move ������ ��
            goalButton.SetActive(false);
            player.SetActive(false);
        }*/
    }
    #endregion
    #region ButtonPosition
    private void PositionOut()
    { // Button Position out
        for (int i = 0; i < yutButton.Length; i++)
        {  // �� ���� �� ���� ��� ��ư Canvas ������ ��ġ
            yutButton[i].transform.position = Camera.main.WorldToScreenPoint(yutButton[i].transform.parent.position);
        }
    }

    public void PositionIn()
    { // Button Position in
        // Character Button click �� �ҷ���, list �ּ� 1�� �̻�
        for (int i = 0; i < yutResultIndex.Count; i++)
        {
            resultIndex = currentIndex + yutArray[yutResultIndex[i]]; // ��ư ��ġ�� ��ġ, �� �� �� �� �� ����

            if (resultIndex >= playerArray.Length)
            { // Goal
                goalButton.SetActive(true);
                continue;
            }
            else if (playerArray.Length > resultIndex)
            { // not Goal
                Vector3 screen = Camera.main.WorldToScreenPoint(playerArray[resultIndex].transform.position);
                yutButton[yutResultIndex[i]].transform.position = screen; // ���� ���� �´� ��ư ������ ����
            }
        }
    }
    #endregion
    #region PlayerButton
    public void CharacterButtonClick(int playerNum)
    { // Canvas - CharacterButton event
        currentIndex = player[playerNum].currentIndex;
        playerArray = player[playerNum].currentArray;

        PositionIn();

        returnButton[playerNum].SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            characterButton[i].SetActive(false);
        }
        // � ���� �����ߴ��� ����
        GameManager.instance.playerNum = playerNum;
    }

    public void ReturnButtonClick()
    { // Canvas - ReturnButton event
        goalButton.SetActive(false);
        PositionOut();
        for (int i = 0; i < 4; i++)
        {
            characterButton[i].SetActive(true);
            returnButton[i].SetActive(false);
        }
    }
    #endregion
    public void TurnPosition(Transform[] pos, int num)
    { // Player ���� ��ġ�� Array ����
        if (pos == pos1)
        {
            if (num == 5)
            { // pos1 -> pos3, 5
                playerArray = pos3;
                currentIndex = 5;
            }
            else if (num == 10)
            { // pos1 -> pos2, 10
                playerArray = pos2;
                currentIndex = 10;
            }
        }
        else if (pos == pos3)
        {
            if (num == 8)
            { // pos3 -> pos4, 8(22 ��ġ)
                playerArray = pos4;
                currentIndex = 8;
            }
        }
        // Catch ������ �� playerArray = pos1�� ����
    }
    public void MoveButton()
    {
        PlayerMovement SelectPlayer = GameManager.instance.players[GameManager.instance.playerNum].GetComponent<PlayerMovement>();
        SelectPlayer.PlayerMove(currentIndex);
    }
}