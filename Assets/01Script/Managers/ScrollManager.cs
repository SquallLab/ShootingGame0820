using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScrollManager : MonoBehaviour
{
    List<IBackgroundScroller> backgroundScrollers;

    // 인스펙터창에서 관리. 
    [SerializeField]
    private float scrollSpeed = 0f;


    private void Start()
    {
        backgroundScrollers = InterfaceFinder.FindObjectsOfInterface<IBackgroundScroller>();

    }


    private void Update()
    {
        foreach(var scroller in backgroundScrollers)
        {
            if(scroller != null)
            {
                scroller.Scroll(Time.deltaTime);
            }
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        //Debug.LogFormat("스크롤 스피드 변경 {0}", newSpeed);
        foreach(var scroller in backgroundScrollers)
        {
            if(scroller is IBackgroundScroller verticalScroller)
            {
                verticalScroller.SetScrollSpeed(newSpeed);
            }
        }
    }
}
