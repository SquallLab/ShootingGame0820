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

public class BossAI : MonoBehaviour, IMovement
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; // ������ ���� � �������� �����ϴ�... 

    private IWeapon[] weapons;
    private IWeapon curWeapon;
    // ���� �߰� ����... 

    public void InitBoss(string name, int newHP, IWeapon[] newWeapons)
    {
        // ui ���� 
        // new hp 
        weapons = newWeapons;

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

        Move(Vector3.down);

        while (true)
        {
            if(transform.position.y <= bossAppearPointY) // �����ߴٸ� 
            {
                Move(Vector3.zero); // �̵��� �����. 
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
            yield return null;
        }

    }
    private IEnumerator BS_Phase02()
    {
        //���ⱳü (2��° ���Ͽ� ���缭 ���� ) 
        curWeapon = weapons[1];
        // �� �� �����ư��鼭 �̵�, 

        Vector2 dir = Vector2.right;

        Move(dir);

        while (true)
        {
            curWeapon.Fire();


            if(transform.position.x <= -2.5f 
                || transform.position.x >= 2.5f)
            {
                dir *= -1f; // ���� ����. 
                Move(dir);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }









    public void Move(Vector2 direction)
    {
    }

    public void SetEnable(bool newEnable)
    {
    }
}
