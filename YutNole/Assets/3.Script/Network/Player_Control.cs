using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player_Control : NetworkBehaviour
{
    [SerializeField] private Button Yut_Btn;

    #region SyncVar
    #endregion
    #region Client
    #endregion
    #region Command
    #endregion
    #region ClientRPC
    #endregion
    #region Unity Callback
    private void Update()
    {
        Debug.Log((int)GM.instance.Player_Num == Server_Manager.instance.Turn_Index);
        Debug.Log(GameManager.instance.hasChance);

        //�����϶� && ������ true�϶� ��ưȰ��ȭ
        if ((int)GM.instance.Player_Num == Server_Manager.instance.Turn_Index && GameManager.instance.hasChance)
        {
            Yut_Btn.interactable = true;
        }
        else
        {
            
            Yut_Btn.interactable = false;
        }
    }
    #endregion
}
