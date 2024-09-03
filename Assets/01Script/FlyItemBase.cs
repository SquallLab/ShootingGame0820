using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//추상 클래스 - 스스로 인스턴스를 생성할수 없어요. 파생클래스(자식)를 만드는데 사용이 되는 목적.
// 공통된 기능(메소드, 필드)과 파생클래스 독특한 자식만의 기능을 구현해야 할때. 
// (다형성을 구현하기 위한 요소)
// 계층 구조가 명확하여 단일 상속 구조를 유지할수 있을때. 


// 몬스터에서 드랍될수있고, 
// 플레이어가 습득할수 있고,
// 월드를 두둥실 떠다닌다. 

public abstract class FlyItemBase : MonoBehaviour, IMovement, IPickuped
{

    public abstract void ApplyEffect(GameObject target);// 구현이 없는 추상 메소드 : 파생클래스에서 구현.


    private bool isInit = false;

    private float flySpeed = 0.7f;
    private Vector2 flyDirection = Vector2.zero;
    private Vector3 flyTargetPos;


    private void Awake()
    {
        SetEnable(true);
        Debug.Log("시작");
    }


    private void Update()
    {
        if (isInit)
            Move(flyDirection);
    }


    public void Move(Vector2 direction)
    {
        transform.Translate(flyDirection * (flySpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
        Debug.Log("시작22");

        if (isInit)
            StartCoroutine("ChangeFlyDirection");
        else
            StopCoroutine("ChangeFlyDirection");
    }

    public void OnPickup(GameObject picker)
    {
        throw new System.NotImplementedException();
    }


    // 스폰이 되고나서 4초에 한번씩 방향 설정해주는 코루틴. 
    IEnumerator ChangeFlyDirection()
    {
        while(true)
        {
            flyTargetPos.x = Random.Range(-2f, 2f);
            flyTargetPos.y = Random.Range(-2f, 2f);
            flyTargetPos.z = 0f;

            flyDirection = (flyTargetPos - transform.position).normalized;
            yield return new WaitForSeconds(4f);
        }
    }
}
