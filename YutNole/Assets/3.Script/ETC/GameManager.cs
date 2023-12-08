using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private WINnLose winNlose;
    [SerializeField] private Unit_Panel unitPanel;

    //���� �� ���� ���� 
    [SerializeField] public GameObject[] P1_Units_Obj; 
    [SerializeField] public GameObject[] P2_Units_Obj;

    public List<int> PlayerIndex;   //�������� �ö� ����

    public int playerNum = 0;   //� �÷��̾ ���õǾ����� �����ϴ� ����
    public bool[] playingPlayer = { false, false, false, false };


    public bool isPlayer1 = true;  //�ϱ��� ����

    public bool isMyTurn;
    public bool isThrew = false;

    public bool hasChance = false; // ��, ��, ����� �� ���� �� �� ��


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

        isPlayer1 = true;

        for(int i = 0; i<P1_Units_Obj.Length; i++)
        {
            P1_Units_Obj[i].SetActive(false);
        }
       
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
