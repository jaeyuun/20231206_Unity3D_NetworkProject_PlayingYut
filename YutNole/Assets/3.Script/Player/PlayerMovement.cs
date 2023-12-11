using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private PlayingYut playingYut;
    private PlayerState playerState;
    private Throw_Yut throw_Yut;
    public Transform[] playerArray; // player�� �ش��ϴ� pos array
    public int currentIndex = 0; // player ���� index, ��ư Ŭ�� �� �̵��� ������ �ٲ�
    public int targetIndex = 0; // ��ư Ŭ�� �� �̵��� index
    public Transform targetPos; // ��ư Ŭ�� �� �̵��� ��ġ

    public float speed = 4f;
    private int moveIndex = 0; // �̵� �ε���
    public bool isBackdo = false;
    public bool isGoal = false;

    private void Awake()
    {
        playingYut = FindObjectOfType<PlayingYut>();
        // UI Player �������� ���� ���߿� �̵�
        playerArray = playingYut.pos1;
        playingYut.playerArray = playerArray;
        playerState = GetComponent<PlayerState>();
        throw_Yut = FindObjectOfType<Throw_Yut>();
    }

    public void PlayerMove(int index)
    { // �� �� �� �� �� ���� ��ư event
        transform.position = playerArray[currentIndex].position; // �÷��̾ ��ġ�� ������
        targetIndex = index; // ��ư�� ������ �� �̵��� �÷��̾� Ÿ�� �ε���

        if (targetIndex - currentIndex == -1)
        { // Backdo
            isBackdo = true;
            moveIndex = currentIndex;
        }
        else if (targetIndex >= playerArray.Length)
        { // Goal 
            isGoal = true;
            moveIndex = playerArray.Length - 1;
        }
        else
        { // ����, ���� �̿�
            moveIndex = targetIndex;
        }

        StartCoroutine(Move_Co());
        
        //���� ������ ��ҿ� ������ �ִٸ�?
        //hasChance =true �����
        //���ٸ�? else
        //if(Yutindex.count > 0) 
        // playingYut.PlayingYutPlus();
        //else turn ���� 
        //������ ���� : ī��Ʈ 0 , ������ȸ X
    }

    private IEnumerator Move_Co()
    {
        for (int i = currentIndex; i <= moveIndex; i++)
        {
            if (isBackdo)
            {
                targetPos = playerArray[i - 1];
            }
            else
            {
                targetPos = playerArray[i];
            }
            while (transform.position != targetPos.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * speed);
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }

        isBackdo = false;
        currentIndex = targetIndex;
        playerArray = playingYut.playerArray;
        playingYut.player[GameManager.instance.playerNum].currentIndex = currentIndex;
        playingYut.player[GameManager.instance.playerNum].currentArray = playerArray;

        foreach (PlayerState player in GameManager.instance.tempPlayers)
        {
            if (player.gameObject == gameObject) continue;
            if (Vector2.Distance(player.transform.position, gameObject.transform.position) < 0.01f)
            {
                if (player.tag == gameObject.tag)
                {
                    //����
                    Debug.Log("��");
                    //player.transform.SetParent(gameObject.transform);
                    //todo.. �����ǹ��ؾ�... ���� ����.
                    //List<PlayerState> list // ���� ���� ����Ʈ
                    //�����ָ� ����Ʈ�� �־�
                    //�������� ����Ʈ���ִ¾ֵ� �ʱ�ȭ����
                    //���������뵵 ����Ʈ���ִ� ����ŭ +1 ���ְ� ��ġ�� �ʱ�ȭ����
                    //���� 2���� // �Ѹ���������    1  (2)  (3)
                     
                }
                else
                {
                    Debug.Log("��");
                    Server_Manager.instance.Catch(player);
                    GameManager.instance.hasChance = true;
                    //todo ������� �ִϸ��̼� �ֱ�
                    //���
                }
            }
        }
        // playingYut.OnDeleteThisIndex.Invoke(playingYut.removeIndex);
        if (playerState.isGoal)
        {
            gameObject.transform.position = playerState.startPos.transform.position;
            playingYut.goalButton.SetActive(false);
            throw_Yut.Yut_Btn_Click(playingYut.removeIndex); // result panel remove
        }

        GameManager.instance.PlayerTurnChange();
    }
}
