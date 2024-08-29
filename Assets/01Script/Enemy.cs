using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��ġ�� �Ǿ �÷��̾� ���� 
// �̵� ���.
// ������ �޴� ���.
// ������ ���. 


public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    private Vector2 moveDir;
    private float moveSpeed= 3f;

    private bool isInit = false;

    // �ۼ�Ʈ�� �����ϱ� ���ؼ� maxHP; 
    private int curHP= 10;
    private int maxHP;

    public bool IsDead { get => curHP <= 0;  }  // =>���ٽ� { return curHP <= 0; }



    // todo : �Ŵ��� �߰��ϰ� ������ �ڵ�. 

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
        // �������� ������ ó���ؾ��ϴ� ������ ��Ƽ�. 
        Debug.LogFormat("���� �޾Ҵ� ���� HP : {0}", curHP);
    }

    private void OnDied()
    {
        // �������� �޴ٰ�, HP�� 0���Ϸ� ��������. 
        // ����. �����۵��.
        Debug.Log("���� ���");
        Destroy(gameObject);

    }

}
