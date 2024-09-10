using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 지정된 방향으로 지정된 속도 지속적으로 이동하는 기능.
// 발사시켜준 주인(주체)owner 과 다른 팀의 대상과 부딪쳤을때, 상대방에게 데미지 전달 역할. 

public class Projectile : MonoBehaviour, IMovement
{
    [SerializeField]
    private float moveSpeed = 10f; // 이동속도.
    private int damage; // 데미지.
    private Vector2 moveDir; // 이동방향
    private GameObject owner; // 발사시켜준 주인정보.
    private string ownerTag;  // 주인의 테그 ( 상대방 팀을 구분하기 위해서 )

    private bool isInit = false; // 정보가 세팅이 되었을때만, 동작. 
    private ProjectileType type; // 자신이 어떤 타입의 프로젝타일인지. 

    // 투사체의 기능을 수행하기 위해서 정보를 세팅해주는 초기화 함수. 
    public void InitProjectile(ProjectileType type,  Vector2 newDir, GameObject newOwner, int newDamage, float newSpeed)
    {
        this.type = type;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner)
            return;
        if (collision.CompareTag(ownerTag))
            return;


        // 화면밖으로 벗어나서 부딪힌경우. 게임스코어 올려주면 X
        // 플레이어가 Enemy맞췄을경우. 게임스코어를 증가. 
        // enemy가 player맞췄을경우. 플레이어의 체력을 깍아주는 역할. 
        if (collision.CompareTag("DestroyArea"))
        {
            //Destroy(gameObject); // 오브젝트풀
            ProjectileManager.Inst.ReturnProjectileToPool(this, type);
        }
        else // 플레이어 맞았냐? enemy가 맞았냐? 
        {
            IDamaged damaged = collision.GetComponent<IDamaged>();
            damaged?.TakeDamage(owner, damage);
            //Destroy(gameObject);
            ProjectileManager.Inst.ReturnProjectileToPool(this, type);
        }
    }

}
