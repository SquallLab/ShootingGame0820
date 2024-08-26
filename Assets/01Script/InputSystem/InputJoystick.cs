using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJoystick : MonoBehaviour, IInputHandler
{
    private MyJoystick joystick;

    private void Awake()
    {
        // FindAnyObjectByType 씬내에 동일한 타입의 오브젝트 1개를 찾아. 
        joystick = FindAnyObjectByType<MyJoystick>();
    }

    public void InitJoystick()
    {
        joystick = FindAnyObjectByType<MyJoystick>();
        if (joystick == null)
            Debug.Log("InputJoystick.cs - InitJoystick() - 조이스틱 참조 실패");
    }

    public Vector2 GetInput()
    {
        return joystick.Direction; // new Vector2(joystick.Direction.x, joystick.Direction.y);
    }
}
