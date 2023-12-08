using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Server_Manager : NetworkBehaviour
{
    public static Server_Manager instance;

    #region SyncVar
    [SyncVar(hook = nameof(OnTurn_Finish))] 
    public int Turn_Index = 1;
    
    #endregion

    #region Client
    #endregion

    #region Command
    [Command(requiresAuthority = false)]
    public void CMD_Turn_Changer()
    {
        int next_Index = (Turn_Index % 2) + 1;
        OnTurn_Finish(Turn_Index, next_Index);
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
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(DelayedFirstTurnSet());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CMD_Turn_Changer();
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
