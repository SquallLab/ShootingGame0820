using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ǰ� �ߴٶ�� �̺�Ʈ�� �߻�. 
public class PlayerHitBox : MonoBehaviour, IDamaged
{

    public static Action<bool> OnPlayerHpIncreased; // true ȸ�� , flase ���� 
    public void TakeDamage(GameObject attacker, int damage)
    {
        OnPlayerHpIncreased?.Invoke(false);
    }
}
