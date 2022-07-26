using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); 
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulation, You Foo !\nYou got a score of " + 
                                scoreKeeper.CalculateScore() + "%";
    }
}
