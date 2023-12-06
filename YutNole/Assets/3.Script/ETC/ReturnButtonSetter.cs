using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButtonSetter : MonoBehaviour
{
    // Button Position�� GameObject Position �����ִ� Script 
    [SerializeField] private RectTransform buttonTrans;
    public Vector3 resultButtonPos; // index pos �������
    public Vector3 characterButtonPos; // ���� ������ �� ĳ���Ϳ� �ߴ� ��ư pos
    public Vector3 returnButtonPos = new Vector3(0.25f, -0.45f, 0); // Return Button

    private void Update()
    {
        SetUp();
    }

    public void SetUp()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + returnButtonPos);
        buttonTrans.position = screenPosition;
    }
}
