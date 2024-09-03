using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//플레이어랑 충돌체크. => 콜라이더랑 리지드바디. 
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DropItem_Jam : MonoBehaviour, IPickuped
{
    public delegate void PickupJam();
    public static event PickupJam OnPickupJam;

    CircleCollider2D col;
    Rigidbody2D rig;

    private bool isSetTarget = false;
    private GameObject target;
    private float pickupTimePer; 


    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out col))
        {
            col.radius = 0.2f;
            col.isTrigger = true;
        }

        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 1.0f;

            // 추후 수정. 
            Vector2 initVelocity = Vector2.zero;
            initVelocity.x = Random.Range(-0.5f, 0.5f);
            initVelocity.y = Random.Range(3.0f, 4.0f);
            rig.AddForce(initVelocity, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if(isSetTarget && target.activeSelf)
        {
            pickupTimePer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.transform.position, pickupTimePer / 2f);

            if(pickupTimePer / 2f > 1f)
            {
                OnPickupJam?.Invoke();
                Destroy(gameObject); // 오브젝트풀 적용 예정. 
            }
        }
    }


    public void OnPickup(GameObject picker)
    {
        rig.gravityScale = 0f;
        rig.velocity = Vector3.zero;
        isSetTarget = true;
        target = picker;
        pickupTimePer = 0f;
    }
}
