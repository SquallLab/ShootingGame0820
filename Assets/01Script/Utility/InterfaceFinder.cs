using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 순수 가상함수로만 이루어진 추상클래스 


//  월드에 있는 특정 interface를 모두 탐색해오기. 
public class InterfaceFinder : MonoBehaviour
{
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {

        //FindObjectsOfType<MonoBehaviour>();  // 성능 좋지 않다. 정렬이라든가 부가적인 기능이 없다.
        //FindObjectsByType<MonoBehaviour>();  // oftype보다 성능이 조금 더 좋고, 데이터 정렬기능등이 있다. 
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        List<T> interfaceObjects = new List<T>();

        foreach (var obj in allObjects)
        {

            // is : obj is T 
            // as 

            if (obj is T interfaceObj) // obj를 T타입으로 캐스팅해서 성공하면, interfaceObj에 참조를 한다.
            {
                interfaceObjects.Add(interfaceObj);
            }
        }

        return interfaceObjects;
    }
}
