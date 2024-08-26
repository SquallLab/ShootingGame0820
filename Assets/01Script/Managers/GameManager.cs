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

    ScrollManager scrollManager;

    private IInputHandler inputHandler;
    private IMovement movementController;

    GameObject obj; // 

    private void Start()
    {
        scrollManager = GameObject.FindAnyObjectByType<ScrollManager>();
        StartCoroutine(GameStart());

        LoadSceneInit(); // 임시: 나중에 씬 변경이 될때, 호출하도록 변경.
    }

    // 씬이 변경이 될때, 
    // 변경된 UI에서 joystick 찾아 참조, 
    // 씬에 따라서 변경되는 Player객체도 찾아서 참조.
    private void LoadSceneInit()
    {
        inputHandler = GetComponent<InputKeyboard>(); // 개발과정 임시.

        obj = FindObjectsByType<PlayerMove>(FindObjectsSortMode.None)[0].gameObject;// 임시

        if(obj != null)
        {
            if (!obj.TryGetComponent<IMovement>(out movementController))
                Debug.Log("GameManager.cs - LoadSceneInit() - movementController참조 실패");
        }

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
            movementController.Move( inputHandler.GetInput() );
        }
    }


    // 게임시작할때 연출을 위해서 딜레이, 
    // 
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("게임 준비");
        yield return new WaitForSeconds(2f);
        scrollManager?.SetScrollSpeed(4f);

        //null 체크 연산자
           

    }
}
