using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ����Ÿ���� Weapon�� �����, 
// ��ü�ذ��鼭 ���� �ֵ���. 
public class PlayerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    private GameObject projectilePrefab;    // ����ü�� ����
    [SerializeField]
    private Transform firePoint;    // ����ü �߻���ġ

    private int numOfProjectiles = 5; // ����ü �߻�Ǵ� ����.
    private float spreadAngle = 5; // ����ü�� ������ �߻� �ɶ�, ���� ���� 
    private float fireRate = 0.3f; // ����ü �߻� ���� 
    private float nextFireTime = 0f;

    private bool isFiring = false; // ���Ⱑ �߻� ������ �����ϴ� ���� 


    float startAngle;
    float angle;
    Quaternion fireRotation;
    GameObject obj;
    Projectile projectComp;



    public void Fire()
    {
        if (Time.time < nextFireTime)
            return; 

        if (isFiring)
        {
            nextFireTime = Time.time + fireRate;

            // ������Ÿ�� ����. 
            // ������Ÿ���� �̵�����(�ʱ�ȭ)

            startAngle = -spreadAngle * (numOfProjectiles - 1) / 2;

            for(int i = 0; i < numOfProjectiles; i++)
            {
                angle = startAngle + spreadAngle * i;

                fireRotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
                Vector2 fireDir = fireRotation * Vector2.up;


                //������Ʈ Ǯ�� -> 
                //obj = Instantiate(projectilePrefab, firePoint.position, fireRotation);
                //projectComp = obj?.GetComponent<Projectile>(); // ?. 
                //projectComp?.InitProjectile(obj.transform.up, gameObject, 1, 10f);
                ProjectileManager.Inst.FireProjectile(ProjectileType.Player01, firePoint.position, fireDir,
                    gameObject, 1, 10f);
            }
        }
    }

    public void SetEnable(bool enable)
    {
        isFiring = enable;
    }

    public void SetOwner(GameObject newOwner)
    {

    }
}
