using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Panel : MonoBehaviour
{
    //�÷��̾� ��
    [SerializeField]
    private GameObject P1_Unit;

    [SerializeField]
    private GameObject P2_Unit;



    public List<GameObject> P1_Units;
    // private GameObject[] P1_Unit;

    [SerializeField]
    public List<GameObject> P2_Units;
    // private GameObject[] P2_Unit;

    [SerializeField]
    private Sprite Goal_sprite;



    private void Start()
    {
        
        for(int i= 0; i<4; i++)
        {
            P1_Units.Add(P1_Unit.transform.GetChild(i).gameObject);
            P2_Units.Add(P2_Unit.transform.GetChild(i).gameObject);
        }



        foreach (GameObject obj in P1_Units)
        {
            obj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);      //return ��ư ��Ȱ��ȭ
            obj.transform.GetChild(1).gameObject.SetActive(false);      //���� img ��Ȱ��ȭ

            GameObject btn = obj.transform.GetChild(0).gameObject;  //ĳ����img 
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Character_Clicked(ref btn); });

            obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Return_Clicked(ref btn); });



        }

        foreach (GameObject obj in P2_Units)
        {
            obj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);      //return ��ư ��Ȱ��ȭ
            obj.transform.GetChild(1).gameObject.SetActive(false);      //���� ��Ȱ��ȭ
        }

    }

    private void Update()
    {
        
    }

    public void Return_Clicked(ref GameObject parentObj)
    {
        //�Ű������� ���� GameObject�� ĳ���� �̹����̹Ƿ� ��� 0���� return��ư

        parentObj.transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            P1_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = true;
        }
    }

    public void Character_Clicked(ref GameObject btn)
    {

        if(GameManager.instance.isThrew)
        {


            //��縻�� ���ڸ��� ������ == �� ���������� Ŭ���� �� ����
            //�ϳ��� �ڸ��� ������ return��ư �ѱ�

            int unitCount = 0;

            //���� �÷��̾� 1�̸�
            for (int i = 0; i < P1_Units.Count; i++)
            {
                if (P1_Units[i].transform.GetChild(0).gameObject.activeSelf)
                {
                    //���� �Ϸ縸 ����..
                    unitCount++;
                    Debug.Log(unitCount);
                }
            }


            if (unitCount >= 4)
            {
                //�� �����ִ� ���
                btn.SetActive(false);
                for (int i = 0; i < 4; i++)
                {
                    P1_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    //p2�ΰ�쵵 �����
                    if (P1_Units[i].transform.GetChild(0).gameObject.Equals(btn))
                    {
                        btn.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        P1_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = false;


                    }
                }



                //������ ���忡 ������ ȭ��ǥ �߰� ��Ź�Ұ�..^^
                //������ ���忡 ������ ������ GameManager�� isThrew ������ false�� �ٲ��� ��Ź��..^^


            }
        }

       


    }





    public void Check_Goal()
    {
        //�����Ѱ��

    }
    

}
