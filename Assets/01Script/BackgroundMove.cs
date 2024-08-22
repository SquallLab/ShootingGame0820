using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public interface IBackgroundScroller
{
    void Scroll(float deltaTime); // 델타타임 입력받아서 오브젝트를 이동시키는 기능.
    void ResetPosition();// 특정 위치까지 내려갔을때, 초기 위치로 자리를 리셋 기능.
    void SetScrollSpeed(float newSpeed); // 스크롤 되는 속도를 변화. 
}


public class BackgroundMove : MonoBehaviour, IBackgroundScroller
{
    [SerializeField]
    float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0f, 12.75f, 0f); // 리셋 되어야 하는 좌표.
    private float resetPositionY = -12.75f; // 화면을 벗어나서 리셋을 시켜야 할지, 말지, 판단하는 높이.

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    public void Scroll(float deltaTime)
    {
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        if(transform.position.y < resetPositionY)
        {
            ResetPosition();
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
