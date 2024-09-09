using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 게임이 진행되는 도중에 지속적으로 몬스터를 생성해 내는 매니저. 

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnTrans;
    [SerializeField]
    private GameObject[] spawnEnemyPrefabs;
    [SerializeField]
    private GameObject[] spawnBossPrefabs;

    public delegate void SpawnFinish();
    public static event SpawnFinish OnSpawnFinish;
    // 일반 몬스터 스폰이 완료 되었다는 이벤트를 발생. 

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;


    private void Awake()
    {
        StartCoroutine(SpawnEnemys());
    }


    GameObject obj;

    IEnumerator SpawnEnemys()
    {
        yield return null;

        while(spawnCount <= 9)
        {
            for (int i = 0; i < spawnTrans.Length; i++)
            {
                obj = Instantiate(spawnEnemyPrefabs[spawnLevel], spawnTrans[i].position, Quaternion.identity);
                if(obj.TryGetComponent<Enemy>(out Enemy enemy))
                    enemy.SetEnable(true);
            }
            spawnCount++;
            yield return new WaitForSeconds(spawnDelta);
        }
        OnSpawnFinish?.Invoke(); //일반 몬스터 스폰 종료가 되면, 
                                 // 보스 등장이라는 경고 메세지를 3초간 띄우고, 
                                 // 보스등장을 시킬.

        yield return new WaitForSeconds(3f);

        obj = Instantiate(spawnBossPrefabs[spawnLevel], new Vector3(0f, 8f, 0f), Quaternion.identity);

        if(obj.TryGetComponent<BossAI>(out BossAI bossAI))
        {
            IWeapon[] weapons = new IWeapon[] { new BossWeapon01(), new BossWeapon03() };
            bossAI.InitBoss("무지막지한 보스", 500, weapons);

            weapons = new IWeapon[] { new BossWeapon02(), new BossWeapon03() };
            bossAI.InitBoss("엄청난 보스", 1000, weapons);

            weapons = new IWeapon[] { new BossWeapon01(), new BossWeapon02() };
            bossAI.InitBoss("굉장한 보스", 1000, weapons);
        }

        spawnLevel++;
        if (spawnLevel >= 3)
            spawnLevel = 0;
        spawnCount = 0;
    }
}
