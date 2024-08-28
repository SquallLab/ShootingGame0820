using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon2 : MonoBehaviour, IWeapon
{
    bool isEnable = false;
    public void Fire()
    {
        if(isEnable)
            Debug.Log("완전 새로운 울트라 캡숑 짱 공격 을 합니다.");
    }

    public void SetEnable(bool enable)
    {
        isEnable = enable;
    }


}
