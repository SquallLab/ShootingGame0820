using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��� ������ �����ð�(2~3�� ����) �������� ���� 
// ������ ���� ��ǥ x( - 2f ~ 2f ), y =0 , z =0;
// ������ �ݺ��� GameManager�� ���ؼ� ��������, �������� �ֵ��� ����� ������. 

// ��ȭ ����..... Ư���� ����� �߰��ؼ�... UI�� ǥ���Ҽ� �ִ� ��� ��� �߰�.. 

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
        StartSpawnMeteo(); // ���� �Ŵ������� ��������. 
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
