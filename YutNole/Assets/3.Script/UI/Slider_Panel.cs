using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Slider_Panel : MonoBehaviour
{

    [SerializeField]
    private Slider P1_Timer_slider;

    [SerializeField]
    private Slider P2_Timer_slider;

    //��Ÿ
    public float TimeLimit = 30; //������ ���ñ��ϴ� ���ѽð�
    public float ThrowTime = 0f;
    public float SliderSpeed = 0.1f;

    private void Start()
    {
        Init();


        //���� �ѱ� -> ������ �ɋ� �ѵ��� �ڵ��ϱ�
        StartCoroutine(Throw_Timer());
    }

    private void Init()
    {
        P1_Timer_slider.GetComponent<Slider>();
        P2_Timer_slider.GetComponent<Slider>();


        //���� 1p�� ���Ǵޱ�
        P1_Timer_slider.value = 0;

        //���� 2p�� ���Ǵޱ�
        P2_Timer_slider.value = 0;

        P1_Timer_slider.maxValue = TimeLimit;
        P2_Timer_slider.maxValue = TimeLimit;

    }


    public IEnumerator Throw_Timer()
    {


        while (true)
        {
            //���� 1p�� ���Ǵޱ�
            P1_Timer_slider.value += SliderSpeed;

            //���� 2p�� ���Ǵޱ�
            P2_Timer_slider.value += SliderSpeed;

            if (ThrowTime > TimeLimit)
            {
                break;
            }

            yield return new WaitForSeconds(SliderSpeed);
        }

    }


}
