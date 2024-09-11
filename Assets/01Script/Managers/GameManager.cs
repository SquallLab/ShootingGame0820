using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 게임의 전반적인 흐름을 관리.
// 1. 게임의 시작, 게임의 중지, 게임을 종료. 
// 2. 사운드 관리자
// 3. 동적로딩 : 에셋 로딩(rom데이터를 Ram공안 불러오기작업)
// 4. 씬 관리 : 씬 변경시에 데이터 주고 받고, 게임종료되면 씬을 변경시키고. 
// 5. (임시) 입력을 받아서, 캐릭터에 전달.  (MVC패턴과 유사.)
// InputHandle로부터 방향 정보를 전달 받아서, 캐릭터의 movement에 전달. 



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
        LoadSceneInit(); // 임시: 나중에 씬 변경이 될때, 호출하도록 변경.
        StartCoroutine(GameStart());
    }

    // 씬이 변경이 될때, 
    // 변경된 UI에서 joystick 찾아 참조, 
    // 씬에 따라서 변경되는 Player객체도 찾아서 참조.
    private void LoadSceneInit()
    {
        inputHandler = GetComponent<InputKeyboard>(); // 개발과정 임시.
        pc = FindAnyObjectByType<PlayerController>();
        scrollManager = FindAnyObjectByType<ScrollManager>();
        spawnManager = FindAnyObjectByType<SpawnManager>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        meteoManager = FindAnyObjectByType<MeteoManager>();


        //obj = FindObjectsByType<PlayerMove>(FindObjectsSortMode.None)[0].gameObject;// 임시

        //if(obj != null)
        //{
        //    if (!obj.TryGetComponent<IMovement>(out movementController))
        //        Debug.Log("GameManager.cs - LoadSceneInit() - movementController참조 실패");
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


    // 게임시작할때 연출을 위해서 딜레이, 
    // 
    IEnumerator GameStart()
    {
        yield return null;
        Debug.Log("게임 데이터 초기화");
        scoreManager?.InitScoreSet();
        scoreManager.OnDiedPlyer += PlayerDieProcess;
        yield return new WaitForSeconds(2f);
        pc?.StartGame(); // 공격을 시작하고, 입력을 받기 시작. 
        Debug.Log("플레이어 컨트롤 On");
        yield return new WaitForSeconds(1f);
        scrollManager?.SetScrollSpeed(4f);
        Debug.Log("배경 스크롤 시작");
        yield return new WaitForSeconds(2f);
        spawnManager?.InitSpawnManager();
        Debug.Log("몬스터 스폰 시작");
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
        // 팝업 오픈 


            
    }
}
