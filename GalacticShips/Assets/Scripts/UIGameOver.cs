using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();  
    }

    // Update is called once per frame
    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetTotalScore();
    }
}
