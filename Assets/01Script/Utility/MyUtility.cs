using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtility : MonoBehaviour
{
    public static Transform FindChildRecursive(Transform parent, string targetName)
    {
        foreach(Transform child in parent)
        {
            if(child.name == targetName)
            {
                return child;
            }
            Transform findTrans = FindChildRecursive(child, targetName); // Àç±ÍÇÔ¼ö. 
            if(findTrans != null)
            {
                return findTrans;
            }
        }
        return null;
    }
}
