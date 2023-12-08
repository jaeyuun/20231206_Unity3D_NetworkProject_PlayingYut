using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Panel : MonoBehaviour
{
    [SerializeField] private PlayingYut playingYut;



    //�÷��̾� ��
    [SerializeField]
    private GameObject P1_Unit;

    [SerializeField]
    private GameObject P2_Unit;

    public bool canBoard = false;   //���ǿ� ������ �ִ°� �Ǵ��ϴ� ����


    public List<GameObject> P1_Units;
    // private GameObject[] P1_Unit;

    [SerializeField]
    public List<GameObject> P2_Units;
    // private GameObject[] P2_Unit;

    [SerializeField]
    private Sprite Goal_sprite;


 

    private void Start()
    {
        playingYut = FindObjectOfType<PlayingYut>();

 
        for (int i= 0; i<4; i++)
        {
            P1_Units.Add(P1_Unit.transform.GetChild(i).gameObject);
            P2_Units.Add(P2_Unit.transform.GetChild(i).gameObject);

            P1_Units[i].name = "Unit" + i;
            P2_Units[i].name = "Unit" + i;

        }

        foreach (GameObject obj in P1_Units)
        {
            //return ��ư
            obj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);


            //���� ȭ��ǥ
            obj.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().raycastTarget = false;

            //���� img
            obj.transform.GetChild(1).gameObject.SetActive(false);

            //ĳ���� �̹���
            GameObject btn = obj.transform.GetChild(0).gameObject;
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Character_Clicked(ref btn); });
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener( playingYut.CharacterButtonClick);

            

            //return ��ư
            obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Return_Clicked(ref btn); });


        }


        foreach (GameObject obj in P2_Units)
        {
            //return ��ư
            obj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);


            //���� ȭ��ǥ
            obj.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().raycastTarget = false;

            //���� img
            obj.transform.GetChild(1).gameObject.SetActive(false);

            //ĳ���� �̹���
            GameObject btn = obj.transform.GetChild(0).gameObject;
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Character_Clicked(ref btn); });

            //return ��ư
            obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Return_Clicked(ref btn); });

        }



        #region ���߿� �ڱ��� �����ϴ� �޼ҵ忡 �ֱ�
        if (GameManager.instance.isPlayer1)
        {
            foreach (GameObject obj in P1_Units)
            {
                obj.transform.GetChild(0).GetComponent<Button>().enabled = true;
            }

            foreach (GameObject obj in P2_Units)
            {
                obj.transform.GetChild(0).GetComponent<Button>().enabled = false;
            }
        }
        else
        {
            foreach (GameObject obj in P2_Units)
            {
                obj.transform.GetChild(0).GetComponent<Button>().enabled = true;
            }

            foreach (GameObject obj in P1_Units)
            {
                obj.transform.GetChild(0).GetComponent<Button>().enabled = false;
            }
        }

        #endregion
    }


    //Return_Btn Ŭ���� ȣ��Ǵ� �޼ҵ�
    public void Return_Clicked(ref GameObject parentObj)
    {
        //�Ű������� ���� GameObject�� ĳ���� �̹����̹Ƿ� ��� 0���� return��ư

        parentObj.transform.GetChild(0).gameObject.SetActive(false);

        if(GameManager.instance.isPlayer1)
        {
            for (int i = 0; i < 4; i++)
            {
                P1_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = true;
                //���� ȭ��ǥ �ٽ� ����
                P1_Units[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                P2_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = true;
                P2_Units[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            }
        }
   
    }





    //ĳ���� Ŭ���� ȣ��Ǵ� �޼ҵ�
    public void Character_Clicked(ref GameObject btn)
    {



        if (GameManager.instance.isThrew)
        {
          
            //��縻�� ���ڸ��� ������ == �� ���������� Ŭ���� �� ����
            //�ϳ��� �ڸ��� ������ return��ư �ѱ�


            int unitCount = 0;

            if(GameManager.instance.isPlayer1)
            {
                for (int i = 0; i < P1_Units.Count; i++)
                {
                    if (P1_Units[i].transform.GetChild(0).gameObject.activeSelf)
                    {                       
                        unitCount++;
                        P1_Units[i].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);                
                    }
                }

            }
            else
            {
                for (int i = 0; i < P2_Units.Count; i++)
                {
                    if (P2_Units[i].transform.GetChild(0).gameObject.activeSelf)
                    {
                
                        unitCount++;
                        P2_Units[i].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                        // player SetActive(true)
                    }
                }

            }




            if (GameManager.instance.isPlayer1)
            {
                if (unitCount >= 4)
                {
                    //�� �����ִ� ���
                    btn.SetActive(false);
                    for (int i = 0; i < 4; i++)
                    {
                        P1_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = false;

                        // ���� �� �÷��̾� �ѱ�
                        if (btn.gameObject.Equals(P1_Units[i].transform.GetChild(0).gameObject))
                        {
                            GameManager.instance.P1_Units_Obj[i].SetActive(true);

                            //������ �������� �� ģ�� ���²�� ���¾�^_^..�� ù��°�� ���õ� �־�
                            GameManager.instance.PlayerIndex.Add(i);        
                            GameManager.instance.playerNum = i;
                            GameManager.instance.playingPlayer[i] = true;
                        }
                    }
                }
                else
                {
                    //�ϳ��� �������� �ִ� ���
                    for (int i = 0; i < 4; i++)
                    {

                        if (P1_Units[i].transform.GetChild(0).gameObject.Equals(btn))
                        {
                            //�Ű����� btn(ĳ����)�� ���ӿ�����Ʈ�� i��° unit�̸� return ��ư �ѱ�
                            btn.transform.GetChild(0).gameObject.SetActive(true);
                            if (btn.gameObject.Equals(P1_Units[i].transform.GetChild(0).gameObject))
                            {
                                canBoard = true;

                                //������ �������� �� ģ�� ���²�� ���¾�^_^..�� ù��° ���� �������� �� ���õȰ�~
                                GameManager.instance.PlayerIndex.Add(i);       
                                GameManager.instance.playerNum = i;
                                GameManager.instance.playingPlayer[i] = true;
                            }
                              
                        }
                        else
                        {
                            //�ٸ� ������Ʈ�� 
                            P1_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = false;
                        }
                    }
                }
            }
            else
            {
                if (unitCount >= 4)
                {
                    //�� �����ִ� ���
                    btn.SetActive(false);
                    for (int i = 0; i < 4; i++)
                    {
                        P2_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {

                        if (P2_Units[i].transform.GetChild(0).gameObject.Equals(btn))
                        {
                            btn.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        else
                        {
                            P2_Units[i].transform.GetChild(0).GetComponent<Button>().enabled = false;


                        }
                    }
                }
            }
                //������ ���忡 ������ ȭ��ǥ �߰� ��Ź�Ұ�..^^ ... �Ϸ� ^^ - ����
                //������ ���忡 ������ ������ GameManager�� isThrew ������ false�� �ٲ��� ��Ź��..^^


            }
        }

     





    public void Check_Goal()    
    {
        //�����Ѱ��

    }
    

}
