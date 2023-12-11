using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

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
    public bool goalButtonClick = false;

    // �� ��� ��������
    public string yutResult;
    public GameObject goalButton; // goal button, resultIndex���� Ŭ �� SetActive(true)
    private Throw_Yut throw_Yut;

    [SerializeField] private List<int> goalResultList = new List<int>();
    public int removeIndex = 0;
    public bool isGoalIn = false;

    private void Start()
    {
        player = GameManager.instance.players;
        throw_Yut = FindObjectOfType<Throw_Yut>();
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
        if (goalButtonClick)
        {
            for (int i = 0; i < 4; i++)
            {
                if (player[i].isGoal) continue;
                if (!player[i].gameObject.activeSelf) continue;
                characterButton[i].SetActive(true); // �÷��̾� ���� ��ư, ������ �÷��̾� ������Ʈ�� ��ư�� Ȱ��ȭ X
            }
            goalButtonClick = false;
        }
        else
        {
            int zeroPlayer = 0; // �ǿ� �����ϴ� ���� ����
            for (int i = 0; i < player.Length; i++)
            {
                if (player[i].currentIndex != 0)
                {
                    zeroPlayer++;
                }
            }
            if (!(yutResult.Equals("Backdo") && zeroPlayer == 0))
            { // ���� �ε����� 0�̸鼭 ������ ��� ������ ���� ����
                for (int i = 0; i < 4; i++)
                {
                    if (player[i].isGoal) continue;
                    if (!player[i].gameObject.activeSelf) continue;
                    characterButton[i].SetActive(true); // �÷��̾� ���� ��ư, ������ �÷��̾� ������Ʈ�� ��ư�� Ȱ��ȭ X
                }
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
    { // Goal Button Event...
        // Goal Count++
        resultIndex = playerArray.Length - 1;

        PositionOut();

        for (int i = 0; i < 4; i++)
        {
            characterButton[i].SetActive(false);
            returnButton[i].SetActive(false);
        }

        goalResultList.Remove(removeIndex);
        yutResultIndex.Remove(removeIndex); // �߰��� ����Ʈ ����
        player[GameManager.instance.playerNum].isGoal = true;
        for (int i = 0; i < player[GameManager.instance.playerNum].carryPlayer.Count; i++)
        {
            player[GameManager.instance.playerNum].carryPlayer[i].isGoal = true;
        }
        goalButtonClick = true;
        isGoalIn = true;

        MoveButton();
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

            if (resultIndex >= playerArray.Length || resultIndex == -1)
            { // Goal
                goalResultList.Add(yutResultIndex[i]);
                if (goalButton.activeSelf) continue;
                goalButton.SetActive(true);
            }
            else if (playerArray.Length > resultIndex)
            { // not Goal
                Vector3 screen = Camera.main.WorldToScreenPoint(playerArray[resultIndex].transform.position);
                yutButton[yutResultIndex[i]].transform.position = screen; // ���� ���� �´� ��ư ������ ����
            }
        }
        if (throw_Yut == null)
        {
            throw_Yut = FindObjectOfType<Throw_Yut>();
        }

        if (goalResultList.Count > 0)
        { // Goal�� ������ ��ư ������ ���������
            removeIndex = goalResultList[0];
            //removeIndex = goalResultList[0];
            for (int i = 0; i < goalResultList.Count - 1; i++)
            {
                if (goalResultList[i] < goalResultList[i + 1])
                {
                    removeIndex = goalResultList[i];
                    //removeIndex = goalResultList[i];
                }
            }

            // OnDeleteThisIndex.Invoke(removeIndex);

            /*if (throw_Yut == null)
            {
                throw_Yut = FindObjectOfType<Throw_Yut>();
            }
            throw_Yut.GoalInButtonPlus();*/
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
            if (player[GameManager.instance.playerNum].isGoal) continue;
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
            if (player[i].isGoal) continue;
            if (!player[i].gameObject.activeSelf) continue;
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