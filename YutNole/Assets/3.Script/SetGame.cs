using UnityEngine;
using UnityEngine.Networking;
using Mirror;


public class SetGame : NetworkBehaviour
{
    private GameObject[] myObject; //0�� ���� 1�� �ű�
    private Transform[] targetPos; // 0123 -> P1 ����ġ / 4567 -> P2 ����ġ
    //�÷��̾� 1 ,2 ������ ��� , ���ġ�� �������� ���������
    private void Start()
    {
        myObject = GameManager.instance.myObject;
        targetPos = GameManager.instance.targetPos;

        // isLocalPlayer üũ�� ���� ���� �÷��̾��� ��쿡�� SpawnOB �޼��� ȣ��
        if (isLocalPlayer)
        {
            SpawnOB(GM.instance.Player_Num);
        }
    }

    [Command]

    private void SpawnOB(Player_Num p)
    {
        // isLocalPlayer�� üũ�ϴ� ��� p�� �ڽ��� �÷��̾� ��ȣ�� ��ġ�ϴ��� Ȯ��

        if (p == Player_Num.P1)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject myOb = Instantiate(myObject[0], targetPos[i].position, Quaternion.identity);

                // isServer üũ ����
                NetworkServer.Spawn(myOb, connectionToClient);

                Debug.Log($"�÷��̾�1 ���� {i}");
            }
        }
        else
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject myOb = Instantiate(myObject[1], targetPos[4 + j].position, Quaternion.identity);

                // isServer üũ ����
                NetworkServer.Spawn(myOb, connectionToClient);

                Debug.Log($"�÷��̾�2 ���� {j}");
            }
        }

    }
}