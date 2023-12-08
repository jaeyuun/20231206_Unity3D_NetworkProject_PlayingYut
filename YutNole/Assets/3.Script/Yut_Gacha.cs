using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yut_Gacha : MonoBehaviour
{
    /*
        1. Ȯ���� ���� �� �ִϸ��̼� ���
    */

    private Animator Yut_ani;

    public string ThrowResult;

 

    [SerializeField] Result_Panel resultPanel;
    [SerializeField] Unit_Panel unitPanel;
    [SerializeField] ThrowYut_Button throwBtn;

    private void Awake()

    {
        Yut_ani = GetComponent<Animator>();
        unitPanel = FindObjectOfType<Unit_Panel>();
        throwBtn = FindObjectOfType<ThrowYut_Button>();
    }

    public void Throwing()
    {
        //������ ��� üũ�ϱ� 
        if (GameManager.instance.hasChance)
        {

            if (GameManager.instance.isPlayer1)
            {
                for (int i = 0; i < 4; i++)
                {
                    unitPanel.P1_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    unitPanel.P2_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = true;
                }
            }

            string[] triggers = { "Mo", "Mo", "Mo", "Mo" };
            // string[] triggers = { "Do", "Do", "Do", "Backdo", "Gae", "Gae", "Gae", "Gae", "Gae", "Gae", "Geol", "Geol", "Geol", "Geol", "Yut", "Mo", "Nack", "Nack" ,"Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo"};
            //string[] triggers = { "Do", "Do", "Do", "Backdo", "Gae", "Gae", "Gae", "Gae", "Gae", "Gae", "Geol", "Geol", "Geol", "Geol", "Yut", "Mo", "Nack", "Nack" };
            ThrowResult = triggers[Random.Range(0, triggers.Length)];

            Yut_ani.SetTrigger(ThrowResult);

            GameManager.instance.hasChance = false;


            //���̸� ���ٹ�����
            if (ThrowResult.Equals("Nack"))
            {
                return;
            }


            //���� �ƹ��͵� ���� �� ���� ������ ��
            int countunits = 0;
            for (int i = 0; i < GameManager.instance.playingPlayer.Length; i++)
            {
                //playingPlayer �迭�� ���ҵ��� ���� false�ΰ� üũ
                if (!GameManager.instance.playingPlayer[i])
                {
                    countunits++;
                }
            }

            
            if (countunits>=4 && ThrowResult.Equals("Backdo"))
            {
                //���� false��
                return;
            }






            resultPanel.Set_Result();
            GameManager.instance.isThrew = true;
            //ĳ���� �����̰� isThrew false�� ����

            if (ThrowResult.Equals("Yut") || ThrowResult.Equals("Mo"))
            {
                GameManager.instance.hasChance = true;
            }
        }
    }

    public void MyTurnButton()
    { // Test�� MyTurn button event
        Debug.Log("MyTurn");
        throwBtn.GetComponent<Image>().sprite = throwBtn.ThrowYut_sprites[1];
        throwBtn.GetComponent<Button>().enabled = true;
        GameManager.instance.hasChance = true;


    }
}
