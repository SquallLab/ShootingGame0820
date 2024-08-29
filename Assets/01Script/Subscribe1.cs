using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscribe1 : MonoBehaviour
{

    BTS bts;

    private void Awake()
    {
        bts = FindAnyObjectByType<BTS>();

        bts.OnConcert += HandleOnConcert; // 구독

        //bts.OnConcert.Invoke("메롱나도 호출할꺼지롱");
    }


    private void HandleOnConcert(string location)
    {
        Debug.LogFormat("야호 BTS가 {0}에서 콘서트를 여는구나.", location);
    }

}
