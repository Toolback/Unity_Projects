using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int totalScore;
    static ScoreKeeper instance;

    //public AudioPlayer getAudioPlayerInstance() { return instance; }

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if(instanceCount > 1)
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetTotalScore() { return totalScore; }
    
    public void SetTotalScore(int newPoints) { totalScore += newPoints; Mathf.Clamp(totalScore, 0, int.MaxValue); }

    public void ResetTotalScore() { totalScore = 0; }
}
