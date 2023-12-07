using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yut_Gacha_Test : MonoBehaviour
{
    /*
        1. Ȯ���� ���� �� �ִϸ��̼� ���
    */

    private Animator Yut_ani;

    public string ThrowResult;

    public bool isChance;

    [SerializeField] Result_Panel resultPanel;

    private void Awake()
    {
        Yut_ani = GetComponent<Animator>();
        isChance = true;
    }

    public void Throwing()
    {
        if (isChance)
        {
            string[] triggers = { "Do", "Do", "Do", "Backdo", "Gae", "Gae", "Gae", "Gae", "Gae", "Gae", "Geol", "Geol", "Geol", "Geol", "Yut", "Mo", "Nack", "Nack" };
            //string[] triggers = { "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo", "Mo"};

            ThrowResult = triggers[Random.Range(0, triggers.Length)];
            Debug.Log(ThrowResult);
            Yut_ani.SetTrigger(ThrowResult);

            if (ThrowResult.Equals("Yut") || ThrowResult.Equals("Mo"))
            {
                isChance = true;
            } else
            {
                isChance = false;
            }

            resultPanel.Set_Result();


        }

    }

    public void MyTurnButton()
    {
        Debug.Log("MyTurn");
        isChance = true;
    }
}
