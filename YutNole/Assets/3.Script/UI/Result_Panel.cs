using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct PlayerStates
{
    //���Ƿ� ����ü �İٽ��ϴ� ���߿� �����ϱ�
    //���߿� ���ӸŴ����� �ֱ�
    public bool isP1;
    public bool isP2;

    public bool hasChance;


    public bool isUnit1_Alive;
    public bool isUnit2_Alive;
    public bool isUnit3_Alive;
    public bool isUnit4_Alive;

    public int GoalCount;

}


public class Result_Panel : MonoBehaviour
{
    //���� ��� 
    [SerializeField] private Image[] Result_imgs;

    [SerializeField]  private Sprite[] Result_sprites;

    [SerializeField] private Yut_Gacha yutGacha;



    private void Start()
    {
        foreach(Image e in Result_imgs)
        {
           
            e.gameObject.SetActive(false);
        }

    }


    private void Update()
    {


       
    }


    public void Set_Result()
    {
        //0���� Ȱ��ȭ ���� �ƴϸ� 0�� Ȱ��ȭ
        //���� 0���� Ȱ��ȭ ���̸� 0���� 1������ ���ε��°� 0������
        int start = 0;
        for (int i = start; i < Result_imgs.Length; i++)
        {
            if (Result_imgs[i].isActiveAndEnabled) start++;
          
        }

        for (int i = start; i < Result_imgs.Length; i++) 
        {
        
            if (!Result_imgs[i].isActiveAndEnabled)
            {
                Debug.Log("1�� Ȱ��ȭ");
                //i���� Ȱ��ȭ ���� �ʾ�����
                Result_imgs[i].gameObject.SetActive(true);
                Throw_Result(ref (Result_imgs[i]));
                break;
            }
            else
            {
                //Ȱ��ȭ �Ǿ� ������


                if (i<4)
                {
                    //�ΰ���������..2�� for�� �Ⱦ��� �ϴ� ����� �����غ���..
                    Result_imgs[i + 1].gameObject.SetActive(true);
                    Result_imgs[i + 1].sprite = Result_imgs[i].sprite;
                    Throw_Result(ref (Result_imgs[i]));
                    break;
                           
                }

            }

        }

    
    }


    public void Throw_Result(ref Image img)
    {

        switch (yutGacha.ThrowResult)
        {

            case "Backdo":
                img.sprite = Result_sprites[0];
                break;

            case "Do":
                img.sprite = Result_sprites[1];
                break;

            case "Gae":
                img.sprite = Result_sprites[2];
                break;

            case "Geol":
                img.sprite = Result_sprites[3];
                break;

            case "Yut":
                img.sprite = Result_sprites[4];
                break;

            case "Mo":
                img.sprite = Result_sprites[5];
                break;

        }


    }
}
