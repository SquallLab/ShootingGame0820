using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ �������� �帧�� ����.
// 1. ������ ����, ������ ����, ������ ����. 
// 2. ���� ������
// 3. �����ε� : ���� �ε�(rom�����͸� Ram���� �ҷ������۾�)
// 4. �� ���� : �� ����ÿ� ������ �ְ� �ް�, ��������Ǹ� ���� �����Ű��. 


public class GameManager : Singleton<GameManager>
{

    ScrollManager scrollManager;


    private void Start()
    {
        scrollManager = GameObject.FindAnyObjectByType<ScrollManager>();

        StartCoroutine(GameStart());
    }


    // ���ӽ����Ҷ� ������ ���ؼ� ������, 
    // 
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("���� �غ�");
        yield return new WaitForSeconds(2f);
        scrollManager?.SetScrollSpeed(4f);

        //null üũ ������
           

    }
}
