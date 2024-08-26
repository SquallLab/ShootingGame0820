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

    ScrollManager scrollManager;

    private IInputHandler inputHandler;
    private IMovement movementController;

    GameObject obj; // 

    private void Start()
    {
        scrollManager = GameObject.FindAnyObjectByType<ScrollManager>();
        StartCoroutine(GameStart());

        LoadSceneInit(); // �ӽ�: ���߿� �� ������ �ɶ�, ȣ���ϵ��� ����.
    }

    // ���� ������ �ɶ�, 
    // ����� UI���� joystick ã�� ����, 
    // ���� ���� ����Ǵ� Player��ü�� ã�Ƽ� ����.
    private void LoadSceneInit()
    {
        inputHandler = GetComponent<InputKeyboard>(); // ���߰��� �ӽ�.

        obj = FindObjectsByType<PlayerMove>(FindObjectsSortMode.None)[0].gameObject;// �ӽ�

        if(obj != null)
        {
            if (!obj.TryGetComponent<IMovement>(out movementController))
                Debug.Log("GameManager.cs - LoadSceneInit() - movementController���� ����");
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
