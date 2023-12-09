using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterArrow_Btn : MonoBehaviour
{
    //YutObject�� �ֱ�

    [SerializeField] private List<Button> buttons;
    [SerializeField] private Unit_Panel unitPanel;


    private void Start()
    {
        buttons.Add(GetComponentInChildren<Button>());
        unitPanel = FindObjectOfType<Unit_Panel>();
    }



    //�� ��ư Ŭ�� ��
    public void YutObject_Clicked()
    {
        GameManager.instance.P1_Units_Obj[GameManager.instance.playerNum].gameObject.SetActive(true);
        
        //���߿� �ι�° ���� ������ �� ���� �� �׽���
        if (unitPanel.canBoard)
        {
            unitPanel.canBoard = false;
        }

    }

}
