using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Panel : MonoBehaviour
{
    //�÷��̾� ��
    [SerializeField]
    private GameObject[] P1_Unit;

    [SerializeField]
    private GameObject[] P2_Unit;

    [SerializeField]
    private Sprite Goal_sprite;

    private void Start()
    {
        foreach (GameObject obj in P1_Unit)
        {
            obj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);      //return ��ư ��Ȱ��ȭ
            obj.transform.GetChild(1).gameObject.SetActive(false);      //���� ��Ȱ��ȭ

            GameObject btn = obj.transform.GetChild(0).gameObject;
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Character_Clicked(ref btn); });
         
        }

        foreach (GameObject obj in P2_Unit)
        {
            obj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);      //return ��ư ��Ȱ��ȭ
            obj.transform.GetChild(1).gameObject.SetActive(false);      //���� ��Ȱ��ȭ
        }

        //for(int i = 0; i<P1_Unit.Length; i++)
        //{
        //    P1_Unit[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Character_Clicked(ref P1_Unit[i]);});
        //}
    }

    private void Update()
    {
        
    }


    public void asdf()
    {
        Debug.Log("zzzzz");
    }
    public void Character_Clicked(ref GameObject btn)
    {
        Debug.Log("������");

        btn.SetActive(false);

    }

    public void Check_Goal()
    {
        //�����Ѱ��

    }
    

}
