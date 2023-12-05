using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yut_Gacha : MonoBehaviour
{
    /*
        1. Ȯ���� ���� �� �ִϸ��̼� ���
    */

    private Animator Yut_ani;

    public string ThrowResult;

    public  PlayerStates playerState;

    [SerializeField] Result_Panel resultPanel;

    private void Awake()
    {
        Yut_ani = GetComponent<Animator>();
        playerState.hasChance = true;
    }

    public void Throwing()
    {

        //������ ��� üũ�ϱ� 

        if(playerState.hasChance)
        {
            playerState.hasChance = false;

            //string[] triggers = { "Do", "Do", "Do", "Backdo", "Gae", "Gae", "Gae", "Gae", "Gae", "Gae", "Geol", "Geol", "Geol", "Geol", "Yut", "Mo", "Nack", "Nack" };
            string[] triggers = { "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo"};

            ThrowResult = triggers[Random.Range(0, triggers.Length)];

            Yut_ani.SetTrigger(ThrowResult);


            if (ThrowResult.Equals("Yut"))
            {
                playerState.hasChance = true;
            }


            if (ThrowResult.Equals("Mo"))
            {
                playerState.hasChance = true;
            }


            resultPanel.Set_Result();


        }

    }
}
