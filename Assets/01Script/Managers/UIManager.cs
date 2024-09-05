using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    Transform canvasTrans;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI jamText;
    TextMeshProUGUI bombText;
    [SerializeField]
    Image[] heartImg;

    Transform tr;

    TextMeshProUGUI ScoreText
    {
        get
        {
            if (scoreText == null)
            {
                tr = MyUtility.FindChildRecursive(canvasTrans, "ScoreText");
                if(tr != null)
                    scoreText = tr.GetComponent<TextMeshProUGUI>();
                //scoreText = canvasTrans.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            }
            return scoreText;
        }
    }

    TextMeshProUGUI JamText
    {
        get
        {
            if (jamText == null)
            {
                //jamText = canvasTrans.Find("JamText").GetComponent<TextMeshProUGUI>();
                tr = MyUtility.FindChildRecursive(canvasTrans, "JamText");
                if (tr != null)
                    jamText = tr.GetComponent<TextMeshProUGUI>();
            }
            return jamText;
        }
    }
    TextMeshProUGUI BombText
    {
        get
        {
            if (bombText == null)
            {
                //bombText = canvasTrans.Find("BombText").GetComponent<TextMeshProUGUI>();
                tr = MyUtility.FindChildRecursive(canvasTrans, "BombText");
                if (tr != null)
                    bombText = tr.GetComponent<TextMeshProUGUI>();
            }
            return bombText;
        }
    }


    private void Awake()
    {
        GameObject obj = GameObject.Find("Canvas");
        canvasTrans = obj.GetComponent<Transform>();

    
    }

    private void OnEnable()
    {
        //이벤트 구독 

        ScoreManager.OnChangeScore += UpdateScoreText;
        ScoreManager.OnChangeBomb += UpdateBombText;
        ScoreManager.OnChangeJamCount += UpdateJamText;
    }

    private void OnDisable()
    {
        ScoreManager.OnChangeScore -= UpdateScoreText;
        ScoreManager.OnChangeBomb -= UpdateBombText;
        ScoreManager.OnChangeJamCount -= UpdateJamText;
    }

    private void UpdateScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }

    private void UpdateBombText(int score)
    {
        BombText.text = "X : " + score.ToString();
    }

    private void UpdateJamText(int score)
    {
        JamText.text = score.ToString();
    }
}
