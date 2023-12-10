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

    
    
    public Transform currentPositon;
    public Transform[] currentArray; // �ڽ��� ���� ��ġ�� �迭
    public int currentIndex = 0; // ���� ��ġ�� �ε���

    // Player NumImage
    public GameObject[] numImage; // numberImage GameObject �������ֱ�

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

    #region Unity Callback
    private void Start()
    {
        SetUp();
    }

    private void Update()
    {
        currentPositon = currentArray[currentIndex];
    }

    private void SetUp()
    { // �÷��̾� ���� ó�� ����
        playingYut = FindObjectOfType<PlayingYut>();
        currentArray = playingYut.pos1;
    }

    #endregion
    #region SyncVar
    [SyncVar (hook = nameof(StartPosTrans))]
    public Transform startPos;
    #endregion
    #region Client
    #endregion
    #region Command
    #endregion
    #region ClientRPC
    #endregion
    #region Hook Method, �ٸ� Ŭ���̾�Ʈ�� �˾ƾ� ��
        private void StartPosTrans(Transform _old, Transform _new)
        {
            startPos = _new;
        }    
        //private void PlayerPosTrans(Transform _old, Transform _new)
        //{
        //    currentPositon = _new;
        //}
        //private void CurrentArrayTrans(Transform[] _old, Transform[] _new)
        //{
        //    currentArray = _new;
        //}
        //private void CurrentIndexTrans(int _old, int _new)
        //{
        //    currentIndex = _new;
        //}
    #endregion
}