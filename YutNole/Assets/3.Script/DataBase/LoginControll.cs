using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginControll : MonoBehaviour
{
    public GameObject JoinUI;
    public InputField id_i;
    public InputField pass_i;
    public InputField nickName;
    [SerializeField] private Text Log;
    [SerializeField] private GameObject Panel;
    public void Login_btn()
    {
        if(id_i.text.Equals(string.Empty) || pass_i.text.Equals(string.Empty))
        {
            Panel.gameObject.SetActive(true);
            Log.text = "���̵� ��й�ȣ�� �Է��ϼ���.";
            StartCoroutine(Set_False());
            return;
        }

        if(SQLManager.instance.Login(id_i.text, pass_i.text))
        {
            //�α��� ����
            user_info info = SQLManager.instance.info;
            ServerChecker.instance.Start_Client();
        }
        else
        {
            //�α��� ����
            Panel.gameObject.SetActive(true);
            Log.text = "���̵� ��й�ȣ�� Ȯ���� �ּ���..";
            StartCoroutine(Set_False());
        }
    }

    public void Join()
    {
        if (id_i.text.Equals(string.Empty) || pass_i.text.Equals(string.Empty) )
        {
            Panel.gameObject.SetActive(true);
            Log.text = "���̵� ��й�ȣ�� �Է��ϼ���.";
            StartCoroutine(Set_False());
            return;
        }
        if (nickName.text.Equals(string.Empty))
        {
            Panel.gameObject.SetActive(true);
            Log.text = "�г����� �Է� ���ּ���";
            StartCoroutine(Set_False());
            return;
        }

        if(SQLManager.instance.Join(id_i.text,pass_i.text , nickName.text))
        {
            Debug.Log($"id : {id_i.text} l pass : {pass_i.text} l nickName : {nickName.text}");
            gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
            Log.text = "���̵� Ȥ�� �г����� �̹� �����մϴ�";
            StartCoroutine(Set_False());
        }
        
    }
    public void OpenJoinUI()
    {
        JoinUI.SetActive(true);
    }

    private IEnumerator Set_False()
    {
        yield return new WaitForSeconds(1.5f);

        Panel.SetActive(false);
    }
}
