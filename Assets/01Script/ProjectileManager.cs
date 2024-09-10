using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ProjectileType
{
    Player01,
    Player02, 
    Player03,
    Boss01,
    Boss02,
    Boss03,
}

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField]
    private GameObject[] projectilePrefabs;
    private Queue<Projectile>[] projectileQueue;
    // ����������, ���������� => 5�������� 
    private int poolSize = 10; // �ѹ��� �����ϴ� ������Ʈ ����. 

    protected override void Awake()
    {
        base.Awake();

        projectileQueue = new Queue<Projectile>[projectilePrefabs.Length]; // ť�� �����ϴ� �迭�� 6��¥���� ������� �ڵ�.
        for(int i = 0;i < projectilePrefabs.Length; i++)
        {
            projectileQueue[i] = new Queue<Projectile>(); // ������ ť�� ����. 
            Allocate((ProjectileType)i);
        }
    }

    GameObject obj;
    Projectile proj;

    // ������Ÿ�� ������� �̸� �����صδ� ����. 
    private void Allocate(ProjectileType type)
    {
        for(int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(projectilePrefabs[(int)type]); // ������ �ν��Ͻ� �����ϰ�,
            if(obj.TryGetComponent<Projectile>(out proj)) // ������ �ν��Ͻ����� ��ũ��Ʈ��ü ã�Ƽ�
            {
                projectileQueue[(int)type].Enqueue(proj); // �ش� ť�� �߰� 
            }
            obj.SetActive(false);
        }
    }

    // �ܺο��� ������ ��ġ�� ������Ÿ�� �����ϰ�, ���ư��� �����, �ӵ��� �������ִ� ���� 
    public void FireProjectile(ProjectileType type, Vector3 spawnPos, Vector2 dir, 
                                GameObject newOwner, int damage, float newSpeed)
    {
        proj = GetProjectileFromPool(type);
        if(proj != null)
        {
            proj.transform.position = spawnPos;
            proj.gameObject.SetActive(true);
            proj.InitProjectile(type, dir, newOwner, damage, newSpeed);
        }
    }

    //����ϱ� ���ؼ� Ǯ���� �ϳ� �������� ���� 
    // Queue �� ������Ʈ�� 0�����, �߰��� �����ؼ� 
    // ����. 
    private Projectile GetProjectileFromPool(ProjectileType type)
    {
        if(projectileQueue[(int)type].Count < 1)
            Allocate(type); // �űԷ� �߰� ����. 

        return projectileQueue[(int)type].Dequeue(); // ť���� �ϳ� ������. 
    }


    // ����� �Ϸ�� ������Ÿ���� Ǯ�� ��ȯ�ϴ� ���� 
    public void ReturnProjectileToPool(Projectile returnProj, ProjectileType type )
    {
        returnProj.gameObject.SetActive(false);
        projectileQueue[(int)type].Enqueue(returnProj);
    }




}
