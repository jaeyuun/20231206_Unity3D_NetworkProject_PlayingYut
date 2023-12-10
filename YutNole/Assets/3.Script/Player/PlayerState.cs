using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerState : NetworkBehaviour
{
    private PlayingYut playingYut;

    // Player�� ��ġ
    public int playerNum = 0; // �÷��̾��� ó�� ��ġ
    public bool isPlaying = false; // ��� ���°� �ƴ� �ǿ� �����ִ���

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
        playingYut.goalButton.GetComponent<Button>().onClick.AddListener(GoalInClick); // ���� ��ư�� ������ ��
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
    [SyncVar (hook = nameof(GoalTrans))]
    public bool isGoal = false; // ���� �ߴ��� �ƴ���
    [SyncVar(hook = nameof(StartTrans))]
    public Transform startPos;
    #endregion
    #region Client
    [Client]
    public void GoalInClick()
    {
        GoalIn_Command();
    }
    #endregion
    #region Command
    [Command(requiresAuthority = false)]
    private void GoalIn_Command()
    { 
        isGoal = true;
        GoalIn_RPC();
    }
    #endregion
    #region ClientRpc
    [ClientRpc]
    public void GoalIn_RPC()
    {
        if (isLocalPlayer)
        {
            startPos.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    #endregion
    #region Hook Method, �ٸ� Ŭ���̾�Ʈ�� �˾ƾ� ��
    /*    private void StartPosTrans(Transform _old, Transform _new)
        {
            startPos = _new;
        }    
        private void PlayerPosTrans(Transform _old, Transform _new)
        {
            currentPositon = _new;
        }
        private void CurrentArrayTrans(Transform[] _old, Transform[] _new)
        {
            currentArray = _new;
        }
        private void CurrentIndexTrans(int _old, int _new)
        {
            currentIndex = _new;
        }*/
    private void GoalTrans(bool _old, bool _new)
    {
        isGoal = _new;
    }

    private void StartTrans(Transform _old, Transform _new)
    {
        startPos = _new;
    }
    #endregion
}