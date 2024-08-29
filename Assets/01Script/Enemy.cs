using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 배치가 되어서 플레이어 방해 
// 이동 기능.
// 데미지 받는 기능.
// 리워드 기능. 


public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    private Vector2 moveDir;
    private float moveSpeed= 3f;

    private bool isInit = false;

    // 퍼센트를 관리하기 위해서 maxHP; 
    private int curHP= 10;
    private int maxHP;

    public bool IsDead { get => curHP <= 0;  }  // =>람다식 { return curHP <= 0; }



    // todo : 매니저 추가하고 수정될 코드. 

    private void Update()
    {
        Move(Vector2.down);
    }

    public void Move(Vector2 direction)
    {
        if (isInit)
        {
            transform.Translate(direction * (moveSpeed * Time.deltaTime));
        }
        
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if( !IsDead )
        {
            curHP -= damage;
            if (curHP > 0)
                OnDamaged();
            else
                OnDied();
        }
    }

    private void OnDamaged()
    {
        // 데미지를 받을때 처리해야하는 로직을 모아서. 
        Debug.LogFormat("공격 받았다 남은 HP : {0}", curHP);
    }

    private void OnDied()
    {
        // 데미지를 받다가, HP가 0이하로 떨어질때. 
        // 연출. 아이템드랍.
        Debug.Log("으앙 쥬금");
        Destroy(gameObject);

    }

}
