using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ͱ� ����ϴ°� ĳġ�ؼ� ������ ��������ٲ�����. 

public class ItemDropManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jamPrefab;

    GameObject obj;

    private void OnEnable()
    {
        Enemy.OnMonsterDied += HandleEnemyDie;

    }
    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleEnemyDie;
    }

    private void HandleEnemyDie(Enemy enemyInfo)
    {
        for(int i = 0; i < 7; i++)
        {
            obj = Instantiate(jamPrefab, enemyInfo.transform.position, Quaternion.identity);
        }
    }

}
