using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ 
public enum BossState
{
    BS_MoveToAppear,//������ġ�� �̵��ϴ� ����,
    BS_Phase01, // ���ڸ����� ������ �ݺ��ϴ� ����,
    BS_Phase02, // �¿�� �̵��ϸ鼭 ������ �ݺ��ϴ� ����, 
}

// AI��ũ��Ʈ��, ������ �и� �ۼ��ص� �ǰ�, 
// 1��ũ��Ʈ���� 2���� ����� ��� ������ ���ٲ���. 
// 

public class BossAI : MonoBehaviour, IMovement, IDamaged
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; // ������ ���� � �������� �����ϴ�... 

    private IWeapon[] weapons;
    private IWeapon curWeapon;
    // ���� �߰� ����... 

    private Vector2 moveDir = Vector2.zero;
    private bool isInit = false;
    private float moveSpeed = 3f;
    private string bossName;
    private int maxHP;
    private int curHP;
    public bool IsDead { get => curHP <= 0; }

    public delegate void BossDiedEvent();
    public event BossDiedEvent OnBossDied;





    public void InitBoss(string name, int newHP, IWeapon[] newWeapons)
    {
        // ui ���� 
        // new hp 
        bossName = name;
        curHP = maxHP = newHP;

        weapons = newWeapons;

        SetEnable(true);
        ChangeState(BossState.BS_MoveToAppear); //  FSM �ʱ���°�; 
    }


    // ���� AI�� �ڷ�ƾ�� ���ؼ� FSM(������ AI)�� ����  �ൿƮ��(������ AI)
    // 
    // ���¸� �������ִ� �޼ҵ� 
    public void ChangeState(BossState newState)
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator BS_MoveToAppear()
    {
        // �Ʒ�(Vector3.down ����) �̵��� ����. 
        // ������ġ�� �����߳�? 
            // Y : ������� ����.
            // N :  ����ؼ� �̵��Ѵ�. 

        //Move(Vector3.down);
        moveDir = Vector2.down;

        while (true)
        {
            if(transform.position.y <= bossAppearPointY) // �����ߴٸ� 
            {
                moveDir = Vector2.zero;
                ChangeState(BossState.BS_Phase01);
            }
            // �������� ���ߴٸ�.... 
            yield return null;
        }
    }

    private IEnumerator BS_Phase01()
    {

        // �� ���� Ȱ��ȭ. 

        curWeapon = weapons[0];
        curWeapon.SetEnable(true);
        while (true)
        {
            curWeapon.Fire();
            yield return new WaitForSeconds(0.4f);
        }

    }
    private IEnumerator BS_Phase02()
    {
        //���ⱳü (2��° ���Ͽ� ���缭 ���� ) 
        curWeapon = weapons[1];
        // �� �� �����ư��鼭 �̵�, 

        moveDir = Vector2.right;

        while (true)
        {
            curWeapon.Fire();


            if(transform.position.x <= -2.5f 
                || transform.position.x >= 2.5f)
            {
                moveDir *= -1f; // ���� ����. 
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void Update()
    {
        if (isInit)
            Move(moveDir);
    }
    public void Move(Vector2 direction)
    {
        transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if (!IsDead)
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

        if(bossState == BossState.BS_Phase01 && (float)curHP / maxHP < 0.5f) // HP�� 50%�̸����� ���̰� �Ǹ�,
        {
            ChangeState(BossState.BS_Phase02);// 2�� ������ �������� ����... 
        }
    }

    private void OnDied()
    {
        OnBossDied?.Invoke();
        // �������� �޴ٰ�, HP�� 0���Ϸ� ��������. 
        // ����. �����۵��.
        Debug.Log("���� ���");
        Destroy(gameObject);

    }
}
