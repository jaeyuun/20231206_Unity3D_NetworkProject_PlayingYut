using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct PlayerStates
{
//����..������ٻ���
    public bool hasChance;

    public bool isUnit1_Alive;
    public bool isUnit2_Alive;
    public bool isUnit3_Alive;
    public bool isUnit4_Alive;

    public int GoalCount;

}



public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private WINnLose winNlose;
    [SerializeField] private Unit_Panel unitPanel;

    public bool isPlayer1 = true;  //�ϱ��� ����

    public bool isMyTurn;
    public bool isThrew = false;

    public bool hasChance;


    public int GoalCount = 0;
    public bool isWin = false;
    public bool isLose = false;

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
        }

        winNlose = FindObjectOfType<WINnLose>();
        unitPanel = FindObjectOfType<Unit_Panel>();

        isPlayer1 = false;
    }




    //ĳ���Ͱ� Goal ������ �����Ҷ� ȣ������ :)
    public void Count_GoalUnit(GameObject unit)
    {

        if(isPlayer1)
        {
            for (int i = 0; i < unitPanel.P1_Units.Count; i++)
            {
                if (unit.name == unitPanel.P1_Units[i].name)
                {
                    unitPanel.P1_Units[i].transform.GetChild(1).gameObject.SetActive(true);
                }
            }

        }   
        else
        {
            for (int i = 0; i < unitPanel.P2_Units.Count; i++)
            {
                if (unit.name == unitPanel.P2_Units[i].name)
                {
                    unitPanel.P2_Units[i].transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
     

        GoalCount++;

        if(GoalCount >= 4)
        {
            isWin = true;
            winNlose.Play_ImgAnimation();
        }
        
    }

 



}
