using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem_Bomb : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreMgr.IncreaseBombCount();
    }


}
