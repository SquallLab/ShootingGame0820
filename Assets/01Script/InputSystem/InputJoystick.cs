using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJoystick : MonoBehaviour, IInputHandler
{
    private MyJoystick joystick;

    private void Awake()
    {
        // FindAnyObjectByType ������ ������ Ÿ���� ������Ʈ 1���� ã��. 
        joystick = FindAnyObjectByType<MyJoystick>();
    }

    public void InitJoystick()
    {
        joystick = FindAnyObjectByType<MyJoystick>();
        if (joystick == null)
            Debug.Log("InputJoystick.cs - InitJoystick() - ���̽�ƽ ���� ����");
    }

    public Vector2 GetInput()
    {
        return joystick.Direction; // new Vector2(joystick.Direction.x, joystick.Direction.y);
    }
}
