using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 열거형 
public enum BossState
{
    BS_MoveToAppear,//전투위치로 이동하는 상태,
    BS_Phase01, // 제자리에서 공격을 반복하는 상태,
    BS_Phase02, // 좌우로 이동하면서 공격을 반복하는 상태, 
}

// AI스크립트랑, 데미지 분리 작성해도 되고, 
// 1스크립트에서 2가지 기능을 모두 구현을 해줄께요. 
// 

public class BossAI : MonoBehaviour, IMovement, IDamaged
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; // 보스가 현재 어떤 상태인지 관리하는... 

    private IWeapon[] weapons;
    private IWeapon curWeapon;
    // 무기 추가 구현... 

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
        // ui 변경 
        // new hp 
        bossName = name;
        curHP = maxHP = newHP;

        weapons = newWeapons;

        SetEnable(true);
        ChangeState(BossState.BS_MoveToAppear); //  FSM 초기상태값; 
    }


    // 보스 AI는 코루틴을 통해서 FSM(간단한 AI)을 구현  행동트리(복잡한 AI)
    // 
    // 상태를 변경해주는 메소드 
    public void ChangeState(BossState newState)
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator BS_MoveToAppear()
    {
        // 아래(Vector3.down 방향) 이동을 시작. 
        // 전투위치에 도달했나? 
            // Y : 전투모드 돌입.
            // N :  계속해서 이동한다. 

        //Move(Vector3.down);
        moveDir = Vector2.down;

        while (true)
        {
            if(transform.position.y <= bossAppearPointY) // 도달했다면 
            {
                moveDir = Vector2.zero;
                ChangeState(BossState.BS_Phase01);
            }
            // 도달하지 못했다면.... 
            yield return null;
        }
    }

    private IEnumerator BS_Phase01()
    {

        // 현 무기 활성화. 

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
        //무기교체 (2번째 패턴에 맞춰서 변경 ) 
        curWeapon = weapons[1];
        // 좌 우 번갈아가면서 이동, 

        moveDir = Vector2.right;

        while (true)
        {
            curWeapon.Fire();


            if(transform.position.x <= -2.5f 
                || transform.position.x >= 2.5f)
            {
                moveDir *= -1f; // 방향 반전. 
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
        // 데미지를 받을때 처리해야하는 로직을 모아서. 
        Debug.LogFormat("공격 받았다 남은 HP : {0}", curHP);

        if(bossState == BossState.BS_Phase01 && (float)curHP / maxHP < 0.5f) // HP가 50%미만으로 떨이게 되면,
        {
            ChangeState(BossState.BS_Phase02);// 2번 패턴이 나오도록 수정... 
        }
    }

    private void OnDied()
    {
        OnBossDied?.Invoke();
        // 데미지를 받다가, HP가 0이하로 떨어질때. 
        // 연출. 아이템드랍.
        Debug.Log("으앙 쥬금");
        Destroy(gameObject);

    }
}
