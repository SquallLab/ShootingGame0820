using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub2 : MonoBehaviour
{
    BTS bts;

    private void Awake()
    {
        bts = FindAnyObjectByType<BTS>();
        bts.OnConcert += TEST;
    }

    private void TEST(string a)
    {
        Debug.Log("�� �� �ܼ�Ʈ�� " + a);
    }
}
