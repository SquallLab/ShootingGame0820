using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터가 사망하는걸 캐치해서 아이템 드랍시켜줄꺼에요. 

public class ItemDropManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jamPrefab;
    [SerializeField]
    private List<GameObject> flyItems;

    GameObject obj;

    private void OnEnable()
    {
        Enemy.OnMonsterDied += HandleEnemyDie;

    }
    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleEnemyDie;
    }


    private int dropRate;
    private void HandleEnemyDie(Enemy enemyInfo)
    {
        for(int i = 0; i < 7; i++)
        {
            obj = Instantiate(jamPrefab, enemyInfo.transform.position, Quaternion.identity);
        }

        dropRate = Random.Range(0, 1000);

        if(dropRate < 10)
            obj = Instantiate(flyItems[0], enemyInfo.transform.position, Quaternion.identity);
        else if (dropRate < 20)
            obj = Instantiate(flyItems[1], enemyInfo.transform.position, Quaternion.identity);
        else if (dropRate < 500)
            obj = Instantiate(flyItems[2], enemyInfo.transform.position, Quaternion.identity);

    }

}
