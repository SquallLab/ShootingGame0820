using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ ����Ǵ� ���߿� ���������� ���͸� ������ ���� �Ŵ���. 

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
    // �Ϲ� ���� ������ �Ϸ� �Ǿ��ٴ� �̺�Ʈ�� �߻�. 

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;


    private void Awake()
    {
        StartCoroutine(SpawnEnemys());
    }


    GameObject obj;
    BossAI bossAI;
    IEnumerator SpawnEnemys()
    {
        yield return null;

        while(spawnCount <= 3)
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
        OnSpawnFinish?.Invoke(); //�Ϲ� ���� ���� ���ᰡ �Ǹ�, 
                                 // ���� �����̶�� ��� �޼����� 3�ʰ� ����, 
                                 // ���������� ��ų.

        yield return new WaitForSeconds(3f);

        obj = Instantiate(spawnBossPrefabs[spawnLevel], new Vector3(0f, 8f, 0f), Quaternion.identity);

        if(obj.TryGetComponent<BossAI>(out bossAI))
        {
            IWeapon[] weapons = new IWeapon[] { new BossWeapon01(), new BossWeapon02() };

            foreach(var weapon in weapons)
            {
                weapon?.SetOwner(obj);
            }

            bossAI.InitBoss("���������� ����", 500, weapons);
            bossAI.OnBossDied += NextLevel;
        }

        spawnLevel++;
        if (spawnLevel >= 3)
            spawnLevel = 0;
        spawnCount = 0;
    }

    public void NextLevel()
    {
        bossAI.OnBossDied -= NextLevel;
        StartCoroutine(SpawnEnemys());

    }

}
