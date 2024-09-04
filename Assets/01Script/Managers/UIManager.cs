using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Transform canvasTrans;


    private void Awake()
    {
        GameObject obj = GameObject.Find("Canvas");
        canvasTrans = obj.GetComponent<Transform>();

        canvasTrans.Find("ScoreText");

    }
}
