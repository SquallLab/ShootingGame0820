using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 피격 했다라는 이벤트만 발생. 
public class PlayerHitBox : MonoBehaviour, IDamaged
{

    public static Action<bool> OnPlayerHpIncreased; // true 회복 , flase 감소 
    public void TakeDamage(GameObject attacker, int damage)
    {
        OnPlayerHpIncreased?.Invoke(false);
    }
}
