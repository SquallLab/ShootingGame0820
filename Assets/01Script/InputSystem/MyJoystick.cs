using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Transform handle;   // 유저의 조작방향을 시각적으로 체크해보기 위해서.
    private Transform pivot;    // 범위를 벗어나는지 확인하기 위한 기준 점. 
    private float distance;  // handle이 조이스틱의 중심부에서 얼만큼 멀어졌는지 거리를 관리.
    private Vector2 direction;

    // direction 의 값을 외부에서 읽기만 가능하도록 Direction생성. 
    // 캡슐화 
    public Vector2 Direction => direction;


    private void Awake()
    {
        InitJoystick(); // 임의로 호출. 추후에는 게임매니저에서 관리하도록. 
    }

    public void InitJoystick()
    {
        handle = transform.Find("Handle");
        pivot = transform.Find("Pivot");
    }



    // 마우스의 위치로 handle 위치를 갱신하고, 
    // 갱신하면서, pivot보다 더 멀어지지는 않게 관리를 하고,
    // direction 의 값을 생성. 
    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    // handle 초기 위치인 로컬기준에 0, 0, 0으로 변경. 
    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
