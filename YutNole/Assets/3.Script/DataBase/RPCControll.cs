using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class RPCControll : NetworkBehaviour
{
    [SerializeField] private TMP_Text chatText;
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private GameObject canvas;
    private static event Action<string> onMessage;
    [SyncVar]
    int currentPlayerIndex = 0;
    //client �� server�� connect �Ǿ��� �� �ݹ� �Լ� 
    public override void OnStartAuthority()
    {

        if (isLocalPlayer)
        {
            canvas.SetActive(true);
        }

        onMessage += newMessage;
    }
    private void newMessage(string mess)
    {
        chatText.text += mess;
    }
    //Ŭ���̾�Ʈ�� Server�� ������ ��
    [ClientCallback]
    private void OnDestroy()
    {
        if (!isLocalPlayer) return;
        onMessage -= newMessage;
    }

    [Client]
    public void Send()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;
        if (string.IsNullOrWhiteSpace(inputfield.text)) return;
        string name = SQLManager.instance.info.User_name;

    CmdSendMessage(inputfield.text , name);
        inputfield.text = string.Empty;
    }

    //RPC�� �ᱹ ClientRpc ��ɾ� < command ��ɾ� < client ��ɾ�
    [Command(requiresAuthority = false)]
    private void CmdSendMessage(string Message , string name)
    {
        Debug.Log("Ŀ������");
        RPCHandleMessage($"[{name}] : {Message}");
    }

    [ClientRpc]
    private void RPCHandleMessage(string message)
    {
        onMessage?.Invoke($"\n{message}");
    }
}

