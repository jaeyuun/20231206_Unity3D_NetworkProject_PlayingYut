using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Canvas_Net : MonoBehaviour
{

    [SerializeField]
    private Slider P1_Timer_slider;

    [SerializeField]
    private Slider P2_Timer_slider;





    //�÷��̾� ��
    [SerializeField]
    private GameObject[] P1_Unit;

    [SerializeField]
    private GameObject[] P2_Unit;

    //��Ÿ
    public float ThrowTime = 0f;
    public float SliderSpeed = 1f;

    private void Start()
    {
        Init();

        StartCoroutine(Throw_Timer());
    }

    private void Init()
    {
        P1_Timer_slider.GetComponent<Slider>();
        P2_Timer_slider.GetComponent<Slider>();
    }


    public IEnumerator Throw_Timer()
    {

        //���� 1p�� ���Ǵޱ�
        P1_Timer_slider.value = 0;

        //���� 2p�� ���Ǵޱ�
        P2_Timer_slider.value = 0;

        while (true)
        {
            //���� 1p�� ���Ǵޱ�
            P1_Timer_slider.value += SliderSpeed;

            //���� 2p�� ���Ǵޱ�
            P2_Timer_slider.value += SliderSpeed;

            if (ThrowTime > 10)
            {
                break;
            }

            yield return new WaitForSeconds(SliderSpeed);
        }
 
    }

}
    