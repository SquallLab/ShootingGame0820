using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ �������� �帧�� ����.
// 1. ������ ����, ������ ����, ������ ����. 
// 2. ���� ������
// 3. �����ε� : ���� �ε�(rom�����͸� Ram���� �ҷ������۾�)
// 4. �� ���� : �� ����ÿ� ������ �ְ� �ް�, ��������Ǹ� ���� �����Ű��. 
// 5. (�ӽ�) �Է��� �޾Ƽ�, ĳ���Ϳ� ����.  (MVC���ϰ� ����.)
// InputHandle�κ��� ���� ������ ���� �޾Ƽ�, ĳ������ movement�� ����. 



public class GameManager : Singleton<GameManager>
{

    

    private IInputHandler inputHandler;
    private PlayerController pc;
    private ScrollManager scrollManager;
    private MeteoManager meteoManager;
    private SpawnManager spawnManager;
    private ScoreManager scoreManager;

    GameObject obj; // 

    private void Start()
    {
        LoadSceneInit(); // �ӽ�: ���߿� �� ������ �ɶ�, ȣ���ϵ��� ����.
        StartCoroutine(GameStart());
    }

    // ���� ������ �ɶ�, 
    // ����� UI���� joystick ã�� ����, 
    // ���� ���� ����Ǵ� Player��ü�� ã�Ƽ� ����.
    private void LoadSceneInit()
    {
        inputHandler = GetComponent<InputKeyboard>(); // ���߰��� �ӽ�.
        pc = FindAnyObjectByType<PlayerController>();
        scrollManager = FindAnyObjectByType<ScrollManager>();
        spawnManager = FindAnyObjectByType<SpawnManager>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        meteoManager = FindAnyObjectByType<MeteoManager>();


        //obj = FindObjectsByType<PlayerMove>(FindObjectsSortMode.None)[0].gameObject;// �ӽ�

        //if(obj != null)
        //{
        //    if (!obj.TryGetComponent<IMovement>(out movementController))
        //        Debug.Log("GameManager.cs - LoadSceneInit() - movementController���� ����");
        //}

        //#if UNITY_EDITOR
        //        inputHandler = GetComponent<InputKeyboard>();
        //#elif UNITY_ANDROID
        //        inputHandler = GetComponent<InputJoystick>();
        //#endif
    }

    private void Update()
    {
        if(inputHandler!= null)
        {
            pc?.CustomUpdate(inputHandler.GetInput());
        }
    }


    // ���ӽ����Ҷ� ������ ���ؼ� ������, 
    // 
    IEnumerator GameStart()
    {
        yield return null;
        Debug.Log("���� ������ �ʱ�ȭ");
        scoreManager?.InitScoreSet();
        scoreManager.OnDiedPlyer += PlayerDieProcess;
        yield return new WaitForSeconds(2f);
        pc?.StartGame(); // ������ �����ϰ�, �Է��� �ޱ� ����. 
        Debug.Log("�÷��̾� ��Ʈ�� On");
        yield return new WaitForSeconds(1f);
        scrollManager?.SetScrollSpeed(4f);
        Debug.Log("��� ��ũ�� ����");
        yield return new WaitForSeconds(2f);
        spawnManager?.InitSpawnManager();
        Debug.Log("���� ���� ����");
        yield return new WaitForSeconds(5f);
        meteoManager.StartSpawnMeteo();


    
    }

    private void PlayerDieProcess()
    {
        StopAllCoroutines();
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return null;
        scoreManager.OnDiedPlyer -= PlayerDieProcess;
        pc?.OverGame();
        scrollManager?.SetScrollSpeed(0f);

        spawnManager?.StopSpawnManager();
        meteoManager?.StopSpawnMeteo();

        yield return new WaitForSeconds(3f);
        // �˾� ���� 


            
    }
}
