using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscribe1 : MonoBehaviour
{

    BTS bts;

    private void Awake()
    {
        bts = FindAnyObjectByType<BTS>();

        bts.OnConcert += HandleOnConcert; // ����

        //bts.OnConcert.Invoke("�޷ճ��� ȣ���Ҳ�����");
    }


    private void HandleOnConcert(string location)
    {
        Debug.LogFormat("��ȣ BTS�� {0}���� �ܼ�Ʈ�� ���±���.", location);
    }

}
