using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yut_Gacha : MonoBehaviour
{
    /*
        1. Ȯ���� ���� �� �ִϸ��̼� ���
    */

    private Animator Yut_ani;

    private void Awake()
    {
        Yut_ani = GetComponent<Animator>();
    }

    public void Throwing()
    {
        string[] triggers = { "Do", "Do", "Do", "Backdo", "Gae", "Gae", "Gae", "Gae", "Gae", "Gae", "Geol", "Geol", "Geol", "Geol", "Yut", "Mo", "Nack", "Nack" };
        Yut_ani.SetTrigger(triggers[Random.Range(0, triggers.Length)]);
    }
}
