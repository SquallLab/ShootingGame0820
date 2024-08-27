using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// void Move() 캐릭터를 이동하는 역할. 
public class PlayerMove : MonoBehaviour, IMovement
{
    private bool isMoving = true; // 켜져 있을때만 이동. 

    [SerializeField]
    private float moveSpeed = 5f; // 플레이어 이동 속도.

    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0f);

    //이동량 
    private Vector3 moveDelta; 

    public void Move(Vector2 direction)
    {
        if (isMoving)
        {
            //moveDelta = new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime;
            moveDelta = new Vector3(direction.x, direction.y, 0f) * (moveSpeed * Time.deltaTime);

            Vector3 newPosition = transform.position + moveDelta; // 현위치 + 이동량 

            // 이동 범위 제한
            newPosition.x = Mathf.Clamp(newPosition.x, minArea.x, maxArea.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minArea.y, maxArea.y);

            transform.position = newPosition;
        }
    }

    public void SetEnable(bool newEnable)
    {
        isMoving = newEnable;
    }
}
