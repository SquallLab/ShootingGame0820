using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void SetOwner(GameObject newOwner);

    void Fire();

    void SetEnable(bool enable);

}
