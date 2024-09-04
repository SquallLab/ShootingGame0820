using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem_Power : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreMgr.PowerUp();
    }


}
