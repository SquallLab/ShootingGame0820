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
    // 참조형변수, 원시형변수 => 5개월차에 
    private int poolSize = 10; // 한번에 생성하는 오브젝트 갯수. 

    protected override void Awake()
    {
        base.Awake();

        projectileQueue = new Queue<Projectile>[projectilePrefabs.Length]; // 큐를 관리하는 배열을 6개짜리로 만들어준 코드.
        for(int i = 0;i < projectilePrefabs.Length; i++)
        {
            projectileQueue[i] = new Queue<Projectile>(); // 각각의 큐를 생성. 
            Allocate((ProjectileType)i);
        }
    }

    GameObject obj;
    Projectile proj;

    // 프로젝타일 사용전에 미리 생성해두는 로직. 
    private void Allocate(ProjectileType type)
    {
        for(int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(projectilePrefabs[(int)type]); // 프리펩 인스턴스 생성하고,
            if(obj.TryGetComponent<Projectile>(out proj)) // 생성된 인스턴스에서 스크립트객체 찾아서
            {
                projectileQueue[(int)type].Enqueue(proj); // 해당 큐에 추가 
            }
            obj.SetActive(false);
        }
    }

    // 외부에서 지정된 위치에 프로젝타일 생성하고, 날아가는 방향과, 속도를 설정해주는 역할 
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

    //사용하기 위해서 풀에서 하나 꺼내오는 역할 
    // Queue 에 오브젝트가 0개라면, 추가로 생성해서 
    // 리턴. 
    private Projectile GetProjectileFromPool(ProjectileType type)
    {
        if(projectileQueue[(int)type].Count < 1)
            Allocate(type); // 신규로 추가 생성. 

        return projectileQueue[(int)type].Dequeue(); // 큐에서 하나 꺼내옴. 
    }


    // 사용이 완료된 프로젝타일을 풀에 반환하는 역할 
    public void ReturnProjectileToPool(Projectile returnProj, ProjectileType type )
    {
        returnProj.gameObject.SetActive(false);
        projectileQueue[(int)type].Enqueue(returnProj);
    }




}
