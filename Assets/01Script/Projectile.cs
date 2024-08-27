using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ �������� ������ �ӵ� ���������� �̵��ϴ� ���.
// �߻������ ����(��ü)owner �� �ٸ� ���� ���� �ε�������, ���濡�� ������ ���� ����. 

public class Projectile : MonoBehaviour, IMovement
{
    [SerializeField]
    private float moveSpeed = 10f; // �̵��ӵ�.
    private float damage; // ������.
    private Vector2 moveDir; // �̵�����
    private GameObject owner; // �߻������ ��������.
    private string ownerTag;  // ������ �ױ� ( ���� ���� �����ϱ� ���ؼ� )

    private bool isInit = false; // ������ ������ �Ǿ�������, ����. 

    // ����ü�� ����� �����ϱ� ���ؼ� ������ �������ִ� �ʱ�ȭ �Լ�. 
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
