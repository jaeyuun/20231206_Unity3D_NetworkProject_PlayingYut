using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Throw_Yut : NetworkBehaviour
{
    /*
        1. �� ������ ��ư�� ������ Ŀ�ǵ忡�� ����� �����
        2. ���� ������� RPC�� ���� ��� Ŭ���̾�Ʈ���� ���� �ִϸ��̼� ���
    */
    private PlayingYut playingYut;
    [SerializeField] private NetworkAnimator Yut_ani;
    [SerializeField] private Result_Yut result;

    #region Unity Callback
    private void Start()
    {
        playingYut = FindObjectOfType<PlayingYut>();
        for (int i = 0; i < playingYut.yutButton.Length; i++)
        {
            int index = i;
            playingYut.yutButton[i].gameObject.GetComponent<Button>().onClick.AddListener(() => Yut_Btn_Click(index));
        }
    }
    #endregion

    #region SyncVar
    [SyncVar(hook = nameof(TriggerChange))] // ������ �����
    private string trigger_ = string.Empty;
    #endregion

    #region Client
    [Client] // ��ư ������ �� Ŭ���̾�Ʈ ���忡�� �������� ��ư ���ȴٰ� ȣ�����ִ� �޼ҵ�
    public void Btn_Click()
    {
        Debug.Log("Btn_Click ȣ���");
        CMDYut_Throwing();

        Server_Manager.instance.CMD_Turn_Changer();
    }

    [Client]
    public void Yut_Btn_Click(int name)
    {
        Debug.Log("Yut_Btn_Click ȣ��");
        CMDYut_Button_Click(name);
    }

    public void ThrowYutResult(string trigger_)
    {
        Debug.Log("ThrowYutResult");
        int index = 0;
        playingYut.yutResult = trigger_;
        switch (trigger_)
        {
            case "Do":
                index = 0;
                break;
            case "Gae":
                index = 1;
                break;
            case "Geol":
                index = 2;
                break;
            case "Yut":
                index = 3;
                break;
            case "Mo":
                index = 4;
                break;
            case "Backdo":
                index = 5;
                break;
        }
        //������ �ƴҶ� && ���� �������� && �ǿ� �����̾��°�� ������ ���ö�(�߰�)
        if ((int)GM.instance.Player_Num == Server_Manager.instance.Turn_Index && !trigger_.Equals("Nack"))
        {
            playingYut.yutResultIndex.Add(index);
        }
        Debug.Log("Count: " + playingYut.yutResultIndex.Count);
    }
    #endregion

    #region Command
    [Command(requiresAuthority = false)] // �������� ������ ������� ������ ����Ʈ�� ���� �� Ŭ���̾�Ʈ�鿡�� �Ѹ��� RPC �޼ҵ� ȣ��
    private void CMDYut_Throwing()
    {
        string[] triggers = { "Do", "Do", "Do", "Backdo", "Gae", "Gae", "Gae", "Gae", "Gae", "Gae", "Geol", "Geol", "Geol", "Geol", "Yut", "Mo" };
        trigger_ = triggers[Random.Range(0, triggers.Length)];
        Debug.Log($"CMDYut_Throwing ȣ�� : {trigger_}");
        // Result_Yut Ŭ������ Set_Result �޼ҵ� ȣ��
        result.Set_Result(trigger_, true);
        RPCYut_Throwing(trigger_);
    }

    [Command(requiresAuthority = false)]
    private void CMDYut_Button_Click(int name)
    {
        string yutTrigger = string.Empty;
        switch (name)
        {
            case 0:
                yutTrigger = "Do";
                break;
            case 1:
                yutTrigger = "Gae";
                break;
            case 2:
                yutTrigger = "Geol";
                break;
            case 3:
                yutTrigger = "Yut";
                break;
            case 4:
                yutTrigger = "Mo";
                break;
            case 5:
                yutTrigger = "Backdo";
                break;
        }
        Debug.Log(yutTrigger);
        result.Set_Result(yutTrigger, false);
    }
    #endregion

    #region ClientRPC
    [ClientRpc] // ������ ������� ���� �ִϸ��̼Ǹ� ������ִ� �޼ҵ�
    private void RPCYut_Throwing(string trigger)
    {
        Debug.Log("RpcRPCYut_Throwing ȣ���");
        Yut_ani.animator.SetTrigger(trigger);

        ThrowYutResult(trigger);
    }
    #endregion
    #region Hook Method
    private void TriggerChange(string _old, string _new)
    {
        trigger_ = _new;
    }
    #endregion
}
