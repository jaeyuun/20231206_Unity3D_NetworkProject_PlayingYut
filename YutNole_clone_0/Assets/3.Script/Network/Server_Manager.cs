using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Server_Manager : NetworkBehaviour
{
    #region SyncVar
    [SyncVar(hook = nameof(OnTurn_Finish))] 
    public int Turn_Index = 1;
    
    #endregion

    #region Client
    #endregion

    #region Command
    [Command]
    public void CMD_Turn_Checker()
    {
        // GetConnectionToClient()�� ���� ���� ����� �����ϴ� Ŭ���̾�Ʈ�� Ŀ�ؼ��� ����ϴ�.
        NetworkConnectionToClient connToClient = connectionToClient;

        // Ŭ���̾�Ʈ�� Ŀ�ؼ��� ���� ��쿡�� ������ �����մϴ�.
        if (connToClient != null)
        {
            int Ran_Num = Random.Range(1, 3);
            OnTurn_Finish(Turn_Index, Ran_Num);
        }
    }

    // ���۽� 1, 2�� �Ѱ��� ��� �������� ���� �����ϴ� ���� > �� �Ŵ������� ���Ӿ����� ������� �� ȣ��
    [Command]
    public void First_TurnSet()
    {
        StartCoroutine(DelayedFirstTurnSet());
    }
    #endregion

    #region ClientRPC
    #endregion

    #region Unity Callback
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CMD_Turn_Checker();
        }
    }
    #endregion

    #region Hook Method
    public void OnTurn_Finish(int _old, int _new)
    {
        Turn_Index = _new;
    }
    #endregion


    private IEnumerator DelayedFirstTurnSet()
    {
        // 1���� �����̸� �� 
        yield return new WaitForSeconds(1f);

        int Ran_Num = Random.Range(1, 3);
        OnTurn_Finish(Turn_Index, Ran_Num);
    }
}
