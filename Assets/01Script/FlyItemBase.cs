using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�߻� Ŭ���� - ������ �ν��Ͻ��� �����Ҽ� �����. �Ļ�Ŭ����(�ڽ�)�� ����µ� ����� �Ǵ� ����.
// ����� ���(�޼ҵ�, �ʵ�)�� �Ļ�Ŭ���� ��Ư�� �ڽĸ��� ����� �����ؾ� �Ҷ�. 
// (�������� �����ϱ� ���� ���)
// ���� ������ ��Ȯ�Ͽ� ���� ��� ������ �����Ҽ� ������. 


// ���Ϳ��� ����ɼ��ְ�, 
// �÷��̾ �����Ҽ� �ְ�,
// ���带 �εս� ���ٴѴ�. 
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class FlyItemBase : MonoBehaviour, IMovement, IPickuped
{

    public abstract void ApplyEffect(GameObject target);// ������ ���� �߻� �޼ҵ� : �Ļ�Ŭ�������� ����.


    private bool isInit = false;
    private float flySpeed = 0.7f;
    private Vector2 flyDirection = Vector2.zero;
    private Vector3 flyTargetPos;
    private Rigidbody2D rig;
    private CircleCollider2D col;
    private CircleCollider2D Col
    {
        get
        {
            if(col == null)
            {
                col = GetComponent<CircleCollider2D>();
            }
            return col;
        }
    }

    private ScoreManager scoreManager;

    protected ScoreManager ScoreMgr
    {
        get
        {
            if (scoreManager == null)
                scoreManager = FindAnyObjectByType<ScoreManager>();
            return scoreManager;
        }
    }




    private void Awake()
    {
        if( TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 0f;
        }

        Col.isTrigger = true;
        Col.radius = 0.23f;

        SetEnable(true);
        Debug.Log("����");
    }


    private void Update()
    {
        if (isInit)
            Move(flyDirection);
    }


    public void Move(Vector2 direction)
    {
        transform.Translate(flyDirection * (flySpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {

        isInit = newEnable;
        Debug.Log("����22");

        if (isInit)
            StartCoroutine("ChangeFlyDirection");
        else
            StopCoroutine("ChangeFlyDirection");
    }

    public void OnPickup(GameObject picker)
    {
        ApplyEffect(picker);
    }


    // ������ �ǰ��� 4�ʿ� �ѹ��� ���� �������ִ� �ڷ�ƾ. 
    IEnumerator ChangeFlyDirection()
    {
        while(true)
        {
            flyTargetPos.x = Random.Range(-2f, 2f);
            flyTargetPos.y = Random.Range(-2f, 2f);
            flyTargetPos.z = 0f;

            flyDirection = (flyTargetPos - transform.position).normalized;
            yield return new WaitForSeconds(4f);
        }
    }



}
