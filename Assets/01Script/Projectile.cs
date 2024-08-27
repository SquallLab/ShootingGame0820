using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 지정된 방향으로 지정된 속도 지속적으로 이동하는 기능.
// 발사시켜준 주인(주체)owner 과 다른 팀의 대상과 부딪쳤을때, 상대방에게 데미지 전달 역할. 

public class Projectile : MonoBehaviour, IMovement
{
    [SerializeField]
    private float moveSpeed = 10f; // 이동속도.
    private float damage; // 데미지.
    private Vector2 moveDir; // 이동방향
    private GameObject owner; // 발사시켜준 주인정보.
    private string ownerTag;  // 주인의 테그 ( 상대방 팀을 구분하기 위해서 )

    private bool isInit = false; // 정보가 세팅이 되었을때만, 동작. 

    // 투사체의 기능을 수행하기 위해서 정보를 세팅해주는 초기화 함수. 
    public void InitProjectile(Vector2 newDir, GameObject newOwner, float newDamage, float newSpeed)
    {
        moveDir = newDir;
        damage = newDamage;
        moveSpeed = newSpeed;

        owner = newOwner;
        ownerTag = owner.tag;

        SetEnable(true);
    }

    public void Move(Vector2 direction)
    {
        if(isInit)
        {
            transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
        }
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }


    private void Update()
    {
        Move(Vector2.zero);
    }
}
