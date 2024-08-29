using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTS : MonoBehaviour
{
    public delegate void Concert(string location); // �븮�� => �޼ҵ带 ��� ȣ�����ټ� �ִ� ����. 
    public event Concert OnConcert; // ��������Ʈ ü�� 

    public Action<string> OnConcert2;
    //�ߵ� ������ ���� ������ �־��.


    private void Awake()
    {
        Invoke("Notice", 5f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnConcert?.Invoke("suwon"); // �����ڿ��� �˸��������ڵ�. 
        }
    }

    private void Notice()
    {
        OnConcert?.Invoke("suwon"); // �����ڿ��� �˸��������ڵ�. 
    }



}
