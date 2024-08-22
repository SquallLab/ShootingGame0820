using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �����Լ��θ� �̷���� �߻�Ŭ���� 


//  ���忡 �ִ� Ư�� interface�� ��� Ž���ؿ���. 
public class InterfaceFinder : MonoBehaviour
{
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {

        //FindObjectsOfType<MonoBehaviour>();  // ���� ���� �ʴ�. �����̶�簡 �ΰ����� ����� ����.
        //FindObjectsByType<MonoBehaviour>();  // oftype���� ������ ���� �� ����, ������ ���ı�ɵ��� �ִ�. 
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        List<T> interfaceObjects = new List<T>();

        foreach (var obj in allObjects)
        {

            // is : obj is T 
            // as 

            if (obj is T interfaceObj) // obj�� TŸ������ ĳ�����ؼ� �����ϸ�, interfaceObj�� ������ �Ѵ�.
            {
                interfaceObjects.Add(interfaceObj);
            }
        }

        return interfaceObjects;
    }
}
