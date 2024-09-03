using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerItemCollector : MonoBehaviour
{
    private Rigidbody2D rig;
    private CircleCollider2D col;

    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 0.0f;
        }
        if(TryGetComponent<CircleCollider2D>(out col))
        {
            col.radius = 2.5f;
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PickupItem"))
        {
            if(collision.TryGetComponent<IPickuped>(out IPickuped pickItem))
            {
                pickItem.OnPickup(transform.root.gameObject); // 연출 추가예정. 
            }
        }
    }

}
