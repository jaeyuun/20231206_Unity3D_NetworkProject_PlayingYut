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
    Nack
}

public class PlayingYut : MonoBehaviour, IStateYutResult
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
    public RectTransform[] yutButton; // ��, ��, ��, ��, �� ��ư

    [SerializeField] private GameObject player; // �ڽ��� ��
    [SerializeField] private GameObject[] playerButton; // character, return
    
    public int currentIndex = 0; // Button �ε���
    public int resultIndex = 0; // ��ư ��ġ�� �ε���
    public List<int> yutResultIndex = new List<int>(); // yut ����� ���� ����, �̵� ��ư Ŭ�� �� Remove
    private int[] yutArray = { 1, 2, 3, 4, 4, -1, 0 }; // �� �� �� �� �� ���� ��

    // �� ��� ��������
    private Yut_Gacha yutGacha;
    public string yutResult;
    public YutState type;

    private void Awake()
    {
        yutGacha = FindObjectOfType<Yut_Gacha>();
        playerArray = pos1;
    }
    
    public void PlayingYutPlus()
    { // �� ������ ��ư Ŭ�� ��
        yutResult = yutGacha.ThrowResult; // �� ���� ���
        type = (YutState)Enum.Parse(typeof(YutState), yutResult);
        yutResultIndex.Add(yutArray[(int)type]); // yutResult�� ���� List�� �̵��� ��ŭ�� ���� �߰�

        Vector3 screen = Camera.main.WorldToScreenPoint(player.transform.position);
        playerButton[0].transform.position = screen; // ���� ���� �´� ��ư ������ ����
    }

    private void YutButtonPosition()
    {
        // ��ư Ȱ��ȭ �� ��ġ ����
        resultIndex = currentIndex + yutArray[(int)type];
        if (yutResult != "Nack")
        {
            // ���� �ƴ� ��
            if (yutResult == "BackDo" && currentIndex == 0)
            {
                // ������ �Ұ���
            }
            else
            {
                Vector3 screen = Camera.main.WorldToScreenPoint(playerArray[resultIndex].transform.position);
                yutButton[(int)type].transform.position = screen; // ���� ���� �´� ��ư ������ ����
            }
        }
    }

    public void CharacterButtonClick()
    { // Canvas - CharacterButton
        playerButton[0].SetActive(false);
        playerButton[1].SetActive(true);

        for (int i = 0; i < yutButton.Length; i++)
        {
            yutButton[(int)type].gameObject.SetActive(true); // ��ư Ȱ��ȭ
        }

        YutButtonPosition();
    }

    public void ReturnButtonClick()
    { // Canvas - ReturnButton
        playerButton[0].SetActive(true);
        playerButton[1].SetActive(false);
    }

    public void YutButtonClick(string name)
    { // Canvas - YutObject - ���������𻪵�
        YutState yutName = (YutState)Enum.Parse(typeof(YutState), name);
        GameObject btn = yutButton[(int)yutName].gameObject;
        Vector3 screen = Camera.main.WorldToScreenPoint(btn.transform.parent.position); // Canvas ������
        btn.transform.position = screen; // ���� ���� �´� ��ư ������ ����

        yutResultIndex.Remove(yutArray[(int)type]); // ����Ʈ ����
        currentIndex += yutArray[(int)type]; // ���� �ε��� ����
    }

    public void TurnPosition(Transform[] pos, int num)
    {
        if (pos == pos1)
        {
            if (num == 5)
            {
                // pos1 -> pos3, 5
                playerArray = pos3;
            } else if (num == 10)
            {
                // pos1 -> pos2, 10
                playerArray = pos2;
            }
        } else if (pos == pos3)
        {
            if (num == 8)
            {
                // pos3 -> pos4, 8(22 ��ġ)
                playerArray = pos4;
            }
        }
        // Catch ������ �� playerArray = pos1��
    }

    #region State YutResult
    public void ResultYutAndMo()
    {
        // Result: Yut, Mo ... Chance++

    }

    public void ResultNack()
    {
        // Result: Nack ... Chance X, 0
        // �ٷ� ���� ������ �Ѿ
    }
    #endregion
}
