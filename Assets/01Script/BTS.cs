using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTS : MonoBehaviour
{
    public delegate void Concert(string location); // 대리자 => 메소드를 대신 호출해줄수 있는 역할. 
    public event Concert OnConcert; // 델리게이트 체인 

    public Action<string> OnConcert2;
    //발동 권한을 나만 갖을수 있어요.


    private void Awake()
    {
        Invoke("Notice", 5f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnConcert?.Invoke("suwon"); // 구독자에게 알림보내는코드. 
        }
    }

    private void Notice()
    {
        OnConcert?.Invoke("suwon"); // 구독자에게 알림보내는코드. 
    }



}
