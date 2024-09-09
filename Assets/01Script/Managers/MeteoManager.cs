using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 경고 라인을 일정시간(2~3초 랜덤) 간격으로 스폰 
// 경고라인 스폰 좌표 x( - 2f ~ 2f ), y =0 , z =0;
// 스폰의 반복은 GameManager에 의해서 꺼질수도, 켜질수도 있도록 만들어 오세요. 

// 심화 과제..... 특수한 기능을 추가해서... UI에 표시할수 있는 어떠한 기능 추가.. 

public class MeteoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject alertLinePrefabs;

    private float spawnDelta = 3f;
    private GameObject obj;

    private AlertLine alertLine;
    private Vector3 spawnPos = Vector3.zero;
    private bool isInit = false;

    private void Awake()
    {
        StartSpawnMeteo(); // 게임 매니저에서 수정예정. 
    }

    public void StartSpawnMeteo()
    {
        StartCoroutine("SpawnMeteo");
    }
    public void StopSpawnMeteo()
    {
        StopCoroutine("SpawnMeteo");
    }

    IEnumerator SpawnMeteo()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(spawnDelta);
            spawnPos.x = Random.Range(-2.2f, 2.2f);

            obj = Instantiate(alertLinePrefabs, spawnPos, Quaternion.identity);
            if(obj.TryGetComponent<AlertLine>(out alertLine))
            {
                alertLine.SpawnedLine();
            }
        }
    }

    public void SetSpawnDelta(float newSpawnDelta)
    {
        spawnDelta = Mathf.Clamp( newSpawnDelta, 0.5f, 3f);
    }
}
