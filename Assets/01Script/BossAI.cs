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

public class BossAI : MonoBehaviour, IMovement
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; // 보스가 현재 어떤 상태인지 관리하는... 

    private IWeapon[] weapons;
    private IWeapon curWeapon;
    // 무기 추가 구현... 

    public void InitBoss(string name, int newHP, IWeapon[] newWeapons)
    {
        // ui 변경 
        // new hp 
        weapons = newWeapons;

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

        Move(Vector3.down);

        while (true)
        {
            if(transform.position.y <= bossAppearPointY) // 도달했다면 
            {
                Move(Vector3.zero); // 이동을 멈춘다. 
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
            yield return null;
        }

    }
    private IEnumerator BS_Phase02()
    {
        //무기교체 (2번째 패턴에 맞춰서 변경 ) 
        curWeapon = weapons[1];
        // 좌 우 번갈아가면서 이동, 

        Vector2 dir = Vector2.right;

        Move(dir);

        while (true)
        {
            curWeapon.Fire();


            if(transform.position.x <= -2.5f 
                || transform.position.x >= 2.5f)
            {
                dir *= -1f; // 방향 반전. 
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
