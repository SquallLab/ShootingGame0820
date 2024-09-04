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

    TextMeshProUGUI ScoreText
    {
        get
        {
            if (scoreText == null)
            {
                scoreText = canvasTrans.Find("ScoreText").GetComponent<TextMeshProUGUI>();
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
                jamText = canvasTrans.Find("JamText").GetComponent<TextMeshProUGUI>();
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
                bombText = canvasTrans.Find("BombText").GetComponent<TextMeshProUGUI>();
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
