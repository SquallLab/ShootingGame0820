using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon2 : MonoBehaviour, IWeapon
{
    bool isEnable = false;
    public void Fire()
    {
        if(isEnable)
            Debug.Log("���� ���ο� ��Ʈ�� ĸ�� ¯ ���� �� �մϴ�.");
    }

    public void SetEnable(bool enable)
    {
        isEnable = enable;
    }


}
