using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ �������� ������ �ӵ� ���������� �̵��ϴ� ���.
// �߻������ ����(��ü)owner �� �ٸ� ���� ���� �ε�������, ���濡�� ������ ���� ����. 

public class Projectile : MonoBehaviour, IMovement
{
    [SerializeField]
    private float moveSpeed = 10f; // �̵��ӵ�.
    private int damage; // ������.
    private Vector2 moveDir; // �̵�����
    private GameObject owner; // �߻������ ��������.
    private string ownerTag;  // ������ �ױ� ( ���� ���� �����ϱ� ���ؼ� )

    private bool isInit = false; // ������ ������ �Ǿ�������, ����. 
    private ProjectileType type; // �ڽ��� � Ÿ���� ������Ÿ������. 

    // ����ü�� ����� �����ϱ� ���ؼ� ������ �������ִ� �ʱ�ȭ �Լ�. 
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


        // ȭ������� ����� �ε������. ���ӽ��ھ� �÷��ָ� X
        // �÷��̾ Enemy���������. ���ӽ��ھ ����. 
        // enemy�� player���������. �÷��̾��� ü���� ����ִ� ����. 
        if (collision.CompareTag("DestroyArea"))
        {
            //Destroy(gameObject); // ������ƮǮ
            ProjectileManager.Inst.ReturnProjectileToPool(this, type);
        }
        else // �÷��̾� �¾ҳ�? enemy�� �¾ҳ�? 
        {
            IDamaged damaged = collision.GetComponent<IDamaged>();
            damaged?.TakeDamage(owner, damage);
            //Destroy(gameObject);
            ProjectileManager.Inst.ReturnProjectileToPool(this, type);
        }
    }

}
