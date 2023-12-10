using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerState : NetworkBehaviour
{
    private PlayingYut playingYut;

    // Player�� ��ġ
    public int playerNum = 0; // �÷��̾��� ó�� ��ġ
    public bool isPlaying = false; // ��� ���°� �ƴ� �ǿ� �����ִ���
    public bool isGoal = false; // ���� �ߴ��� �ƴ���
    public Transform[] currentArray; // �ڽ��� ���� ��ġ�� �迭

    public int currentIndex = 0; // ���� ��ġ�� �ε���
    public Transform startPos;
    // public Player_Num myNum;

    // Player Button
    private GameObject[] charBtn = null;
    private GameObject[] returnBtn = null;
    public GameObject characterButton = null;
    public GameObject returnButton = null;

    // Player NumImage
    public GameObject[] numImage; // numberImage GameObject �������ֱ�

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("LocalPlayer");
    }

    #region Unity Callback
    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    { // �÷��̾� ���� ó�� ����
        playingYut = FindObjectOfType<PlayingYut>();
        currentArray = playingYut.pos1;
    }

   
    #endregion
    #region SyncVar
    #endregion
    #region Client
    #endregion
    #region Command
    #endregion
    #region ClientRPC
    #endregion
    #region Hook Method, �ٸ� Ŭ���̾�Ʈ�� �˾ƾ� ��
    private void PlayerStateTrans(Transform[] _old, Transform[] _new)
    {
        currentArray = _new;
    }
    #endregion
}