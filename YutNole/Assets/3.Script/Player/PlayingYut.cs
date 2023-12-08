using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum YutState
{
    Do = 0,
    Gae,
    Geol,
    Yut,
    Mo,
    Backdo,
}

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
    [SerializeField] private GameObject player; // �ڽ��� ��, 4�� ���� ����
    // ĳ���͸��� �پ��ִ� ��ư
    public GameObject charcterButton; // character, 4�� ����
    public GameObject returnButton; // return, 4�� ����

    public int currentIndex = 0; // Button ��ġ ��ų ���� �ε���, player �����ǰ� �����ؾ� ��
    public int resultIndex = 0; // ��ư ��ġ�� �ε���
    public List<int> yutResultIndex = new List<int>(); // yut ����� ���� ����, �̵� ��ư Ŭ�� �� Remove, Nack�̸� Add ����
    private int[] yutArray = { 1, 2, 3, 4, 5, -1 }; // �� �� �� �� �� ����

    // �� ��� ��������
    public Yut_Gacha yutGacha; // ���߿� Yut_Gacha�� �ٲ��ֱ�
    public string yutResult;

    public GameObject goalButton; // goal button, resultIndex���� Ŭ �� SetActive(true)

    private void Awake()
    {
        yutGacha = FindObjectOfType<Yut_Gacha>(); // ���߿� Yut_Gacha�� �ٲ��ֱ�
        playerArray = pos1;
    }
    
    public void PlayingYutPlus()
    { // �� ������ ��ư event
        yutResult = yutGacha.ThrowResult; // �� ���� ���

        if (!yutResult.Equals("Nack") && !(yutResult.Equals("Backdo") && currentIndex == 0))
        { // ���̰ų� ���� �ε����� 0�̸鼭 ������ ��� ������ ���� ����
            YutState type = (YutState)Enum.Parse(typeof(YutState), yutResult);
            yutResultIndex.Add(yutArray[(int)type]); // yutResult�� ���� List�� �̵��� ��ŭ�� ���� �߰�
            charcterButton.SetActive(true); // �÷��̾� ���� ��ư, ������ �÷��̾� ������Ʈ�� ��ư�� Ȱ��ȭ X
        }
        // Nack�� ��, �ε��� 0�� �� ������ ��
    }
    
    public void YutButtonClick(string name)
    { // Canvas - YutObject - ��, ��, ��, ��, ��, ���� event
        charcterButton.SetActive(false);
        returnButton.SetActive(false);

        YutState yutName = (YutState)Enum.Parse(typeof(YutState), name); // ��ư�� ���� �޶���
        // GameObject yutObject = yutButton[(int)yutName].gameObject;

        yutResultIndex.Remove(yutArray[(int)yutName]); // �߰��� ����Ʈ ����
        currentIndex += yutArray[(int)yutName]; // ���� �ε��� ����Ʈ ������ ���� ������ ����
        TurnPosition(playerArray, currentIndex); // ���� ��ġ �迭 ����

        PositionOut();
        goalButton.SetActive(false);

        if (yutResultIndex.Count > 0)
        { // ����Ʈ�� ������ ��
            // �������� ���� ĳ���� ���� ���� Ȱ��ȭ
            if (!player.GetComponent<PlayerMovement>().isGoal)
            {
                charcterButton.SetActive(true);
            }
        }
    }
    #region Goal Button
    public void GoalButtonClick()
    { // Goal Button Event
        // Goal Count++
        resultIndex = playerArray.Length - 1;
        PositionOut();
        charcterButton.SetActive(false);
        returnButton.SetActive(false);

        if (Vector3.Distance(player.transform.position, playerArray[resultIndex - 1].position) <= 0.01f)
        { // move ������ ��
            goalButton.SetActive(false);
            player.SetActive(false);
        }
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
        YutState yutType = YutState.Backdo; // �ʱ�ȭ
        for (int i = 0; i < yutResultIndex.Count; i++)
        {
            resultIndex = currentIndex + yutResultIndex[i]; // ��ư ��ġ�� ��ġ
            if (yutResultIndex[i] != -1)
            {
                yutType = (YutState)(yutResultIndex[i] - 1);
            }
            if (resultIndex >= playerArray.Length)
            { // Goal
                goalButton.SetActive(true);
                continue;
            }
            else if (playerArray.Length > resultIndex)
            { // not Goal
                Vector3 screen = Camera.main.WorldToScreenPoint(playerArray[resultIndex].transform.position);
                yutButton[(int)yutType].transform.position = screen; // ���� ���� �´� ��ư ������ ����
            }
        }
    }
    #endregion
    #region PlayerButton
    public void CharacterButtonClick()
    { // Canvas - CharacterButton event
        PositionIn();
        returnButton.SetActive(true);
        charcterButton.SetActive(false);
    }

    public void ReturnButtonClick()
    { // Canvas - ReturnButton event
        charcterButton.SetActive(true);
        goalButton.SetActive(false);
        PositionOut();
        returnButton.SetActive(false);
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
            } else if (num == 10)
            { // pos1 -> pos2, 10
                playerArray = pos2;
                currentIndex = 10;
            }
        } else if (pos == pos3)
        {
            if (num == 8)
            { // pos3 -> pos4, 8(22 ��ġ)
                playerArray = pos4;
                currentIndex = 8;
            }
        }
        // Catch ������ �� playerArray = pos1�� ����
    }

}
