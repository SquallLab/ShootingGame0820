using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossWeaponBase //???????????????? 
{
    protected GameObject owner;
}
public class BossWeapon01 : BossWeaponBase, IWeapon
{
    Vector3 firePos; 
    int numOfProj = 5;
    float spreadAngle = 15f;
    public void Fire()
    {
        // 1무기 공격 방식 구현 
        firePos = owner.transform.position;

        for(int i = 0; i < numOfProj; i++)
        {
            float angle = spreadAngle * (i - (numOfProj - 1) / 2f);
            Vector2 fireDir = Quaternion.Euler(0, 0, angle) * Vector2.down;

            ProjectileManager.Inst.FireProjectile(ProjectileType.Boss01, firePos, fireDir, owner, 1, 6f);

        }
    }

    public void SetEnable(bool enable)
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}

public class BossWeapon02 : BossWeaponBase, IWeapon // ? 모노비헤이어를 상속받지 않았기 때문에. 
{
    public void Fire()
    {
        Vector3 firePos = owner.transform.position;

        int numOfProj = 36;
        float angleDelta = 360f / numOfProj;
        float startAngle = Random.Range(-10f, 10f);

        for(int i = 0; i < numOfProj; i++)
        {
            float spawnAngle = i * angleDelta + startAngle;
            Vector2 fireDir = Quaternion.Euler(0f, 0f, angleDelta) * Vector2.down;
            ProjectileManager.Inst.FireProjectile(ProjectileType.Boss02, firePos, fireDir, owner, 1, 2f);
        }
    }

    public void SetEnable(bool enable)
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner =newOwner;
    }
}

public class BossWeapon03 : BossWeaponBase,  IWeapon
{
    public void Fire()
    {
        // 3 여러분이 구상해서 구현. 집에가서 과제 
        
    }

    public void SetEnable(bool enable)
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}
