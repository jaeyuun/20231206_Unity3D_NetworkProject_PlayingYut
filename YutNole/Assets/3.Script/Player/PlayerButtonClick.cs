using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonClick : MonoBehaviour
{
    // Player Canvas�� �޷��ִ� ��ư ���
    [SerializeField] private GameObject[] characterButton;
    [SerializeField] private GameObject returnButton;

    public void CharacterButton()
    {
        returnButton.SetActive(true);
        // ����
        foreach (GameObject g in characterButton)
        {
            g.SetActive(false);
        }
    }
    public void ReturnButton()
    {
        foreach (GameObject g in characterButton)
        {
            
            g.SetActive(true);
        }
        returnButton.SetActive(false);
    }
}
