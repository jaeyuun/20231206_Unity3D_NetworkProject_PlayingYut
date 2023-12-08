using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SM_Spawner : NetworkBehaviour
{
    // �������� �Ҵ��� ����
    public GameObject serverManagerPrefab;

    [Command(requiresAuthority = false)]
    public void InitServer_Manager()
    {
        // ���������� ����ǵ���
        if (isServer)
        {
            // Spawn �޼��带 ����Ͽ� �������� ��ȯ
            GameObject serverManager = Instantiate(serverManagerPrefab);
            NetworkServer.Spawn(serverManager);
        }
    }
}
