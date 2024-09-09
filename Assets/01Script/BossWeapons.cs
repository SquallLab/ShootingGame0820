using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon01 : IWeapon
{
    public void Fire()
    {
        // 1무기 공격 방식 구현 
    }

    public void SetEnable(bool enable)
    {
    }
}

public class BossWeapon02 : IWeapon // ? 모노비헤이어를 상속받지 않았기 때문에. 
{
    public void Fire()
    {
        // 2무기 공격 방식 구현
    }

    public void SetEnable(bool enable)
    {
    }
}

public class BossWeapon03 : IWeapon
{
    public void Fire()
    {
        // 3 무기 공격 방식 구현. 
        
    }

    public void SetEnable(bool enable)
    {
    }
}
