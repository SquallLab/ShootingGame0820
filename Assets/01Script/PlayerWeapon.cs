using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 여러타입의 Weapon을 만들고, 
// 교체해가면서 쓸수 있도록. 
public class PlayerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    private GameObject projectilePrefab;    // 투사체의 종류
    [SerializeField]
    private Transform firePoint;    // 투사체 발사위치

    private int numOfProjectiles = 5; // 투사체 발사되는 갯수.
    private float spreadAngle = 5; // 투사체가 여러발 발사 될때, 사이 간격 
    private float fireRate = 0.3f; // 투사체 발사 간격 
    private float nextFireTime = 0f;

    private bool isFiring = false; // 무기가 발사 중인지 관리하는 변수 


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

            // 프로젝타일 생성. 
            // 프로젝타일의 이동방향(초기화)

            startAngle = -spreadAngle * (numOfProjectiles - 1) / 2;

            for(int i = 0; i < numOfProjectiles; i++)
            {
                angle = startAngle + spreadAngle * i;

                fireRotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
                Vector2 fireDir = fireRotation * Vector2.up;


                //오브젝트 풀링 -> 
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
