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
     * Yut�� ���� �÷��̾� �̵� �� ��ư ����, ȭ��ǥ ���� �� ȭ��ǥ ������ �� �÷��̾� ����
        Trun�� position�� �ɸ��� Transform �迭�� �ٲ��ֱ�
     */
    [SerializeField] private Canvas canvas; // ��ư ������� Canvas, �� �����¿� Canvas ��� ����
    [SerializeField] private Transform[] pos1;
    [SerializeField] private Transform[] pos2;
    [SerializeField] private Transform[] pos3;
    [SerializeField] private Transform[] pos4;

    [SerializeField] private GameObject[] yutPrefabs; // ��, ��, ��, ��, �� ��ư
    [SerializeField] private GameObject player; // �ڽ��� ��
    [SerializeField] private GameObject[] playerButton; // character, return
    
    private bool isTurn = false; // 5, 10, 22 ��ġ�� ���� �� isTurn = true
    private bool isGoal = false; // 0�� �������� �� isGoal = true

    private int currentIndex = 0; // ���� ��ġ�� �ε���
    public List<int> yutResultIndex = new List<int>(); // yut ����� ���� ����, �̵� ��ư Ŭ�� �� Remove
    private int[] yutList = { 1, 2, 3, 4, 4, -1, 0 };

    // �� ��� ��������
    private Yut_Gacha yutGacha;
    private string yutResult;

    private void Awake()
    {
        yutGacha = FindObjectOfType<Yut_Gacha>();
    }

    public void PlayingYutPlus()
    {
        yutResult = yutGacha.ThrowResult;
        YutState type = (YutState)Enum.Parse(typeof(YutState), yutResult);
        switch (yutResult)
        {
            case "Backdo":
            case "Do":
            case "Gae":
            case "Geol":
                yutResultIndex.Add(yutList[(int)type]);
                break;
            case "Yut":
            case "Mo":
                yutResultIndex.Add(yutList[(int)type]);
                ResultYutAndMo();
                break;
            case "Nack":
                yutResultIndex.Add(yutList[(int)type]);
                break;
        }
        
        switch (currentIndex)
        {
            case 0:
                break;
            case 10:
            case 15:
            case 22:
                TurnPosition(currentIndex);
                break;
        }

        playerButton[0].SetActive(true);
    }

    private void TurnPosition(int index)
    {
        isTurn = true;
        yutResultIndex.Add(currentIndex);
        // pos1 -> pos2, 10

        // pos1 -> pos3, 5

        // pos3 -> pos4, 22

    }

    public void ResultButtonClick()
    {
        // ���������� ��ư ������ �� �̺�Ʈ, Ŭ���� �÷��̾� ��ġ�� ���� �޶���
        List<GameObject> buttonList = new List<GameObject>();        
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
