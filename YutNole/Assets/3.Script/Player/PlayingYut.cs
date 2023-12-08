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
    [SerializeField] private GameObject[] player; // �ڽ��� ��
    // ĳ���͸��� �پ��ִ� ��ư
    public GameObject[] charcterButton; // character
    public GameObject[] returnButton; // return

    public int currentIndex = 0; // Button ��ġ ��ų ���� �ε���, player �����ǰ� �����ؾ� ��
    public int resultIndex = 0; // ��ư ��ġ�� �ε���
    public List<int> yutResultIndex = new List<int>(); // yut ����� ���� ����, �̵� ��ư Ŭ�� �� Remove, Nack�̸� Add ����
    private int[] yutArray = { 1, 2, 3, 4, 5, -1 }; // �� �� �� �� �� ����

    // �� ��� ��������
    public Yut_Gacha yutGacha;
    public string yutResult;

    public GameObject goalButton; // goal button, resultIndex���� Ŭ �� SetActive(true)

    private void Awake()
    {
        yutGacha = FindObjectOfType<Yut_Gacha>();
        playerArray = pos1;
        
    }

    private void Start()
    {
        //player = new GameObject[4];
        //Debug.Log(GameManager.instance.isPlayer1);
        //if (GameManager.instance.isPlayer1)
        //{
        //    for (int i = 0; i < player.Length; i++)
        //    {
        //        player[i] = GameObject.FindGameObjectWithTag("Player1").transform.GetChild(i).gameObject;
        //    }
        //}
        //else
        //{ // Player2
        //    for (int i = 0; i < player.Length; i++)
        //    {
        //        player[i] = GameObject.FindGameObjectWithTag("Player2").transform.GetChild(i).gameObject;
        //    }
        //}
    }

    public void PlayingYutPlus()
    { // �� ������ ��ư event
        yutResult = yutGacha.ThrowResult; // �� ���� ���

        if (!yutResult.Equals("Nack") && !(yutResult.Equals("Backdo") && currentIndex == 0))
        { // ���̰ų� ���� �ε����� 0�̸鼭 ������ ��� ������ ���� ����
            YutState type = (YutState)Enum.Parse(typeof(YutState), yutResult);
            yutResultIndex.Add(yutArray[(int)type]); // yutResult�� ���� List�� �̵��� ��ŭ�� ���� �߰�

            for (int i = 0; i < 4; i++)
            {
                if (GameManager.instance.playingPlayer[i])
                {
                    Debug.Log("Button?");
                    charcterButton[i].SetActive(true); // �÷��̾� ���� ��ư, ������ �÷��̾� ������Ʈ�� ��ư�� Ȱ��ȭ X
                }
            }
        }
        // Nack�� ��, �ε��� 0�� �� ������ ��
    }
    
    public void YutButtonClick(string name)
    { // Canvas - YutObject - ��, ��, ��, ��, ��, ���� event
        for (int i = 0; i < 4; i++)
        {
            charcterButton[i].SetActive(false);
            returnButton[i].SetActive(false);
        }

        YutState yutName = (YutState)Enum.Parse(typeof(YutState), name); // ��ư�� ���� �޶���
        // GameObject yutObject = yutButton[(int)yutName].gameObject;

        yutResultIndex.Remove(yutArray[(int)yutName]); // �߰��� ����Ʈ ����
        currentIndex += yutArray[(int)yutName]; // ���� �ε��� ����Ʈ ������ ���� ������ ����
        TurnPosition(playerArray, currentIndex); // ���� ��ġ �迭 ����

        PositionOut();
        if (goalButton.activeSelf)
        {
            goalButton.SetActive(false);
        }

        if (yutResultIndex.Count > 0)
        { // ����Ʈ�� ������ ��
          // �������� ���� ĳ���� ���� ���� Ȱ��ȭ
            for (int i = 0; i < 4; i++)
            {
                if (!GameManager.instance.playingPlayer[i])
                {
                    charcterButton[i].SetActive(true);
                }
            }
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
            charcterButton[i].SetActive(false);
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
    public void CharacterButtonClick(int playerNum)
    { // Canvas - CharacterButton event
        PositionIn();
        for (int i = 0; i < 4; i++)
        {
            returnButton[i].SetActive(true);
            charcterButton[i].SetActive(false);
        }
        // � ���� �����ߴ��� ����
        GameManager.instance.playerNum = 0;
    }

    public void ReturnButtonClick()
    { // Canvas - ReturnButton event
        goalButton.SetActive(false);
        PositionOut();
        for (int i = 0; i < 4; i++)
        {
            charcterButton[i].SetActive(true);
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
