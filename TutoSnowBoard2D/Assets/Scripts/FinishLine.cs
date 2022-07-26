using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    [SerializeField] ParticleSystem WinEffect;


    void OnTriggerEnter2D(Collider2D collision)
    {
        // If Player pass finish line then :
        // Increment stats 
        // Restart at scene index 0 after 2sec delay;
        if (collision.tag == "Player")
        {
            PlayerControls pC = collision.gameObject.GetComponent<PlayerControls>();
            WinEffect.Play();
            bool Win = pC.hasWinThisGame;
            int WinCount = pC.WinCount;
            int GameCount = pC.GameCount;
            float Delay = pC.timeToRespawn;
            Win = true;
            WinCount += 1;
            GameCount += 1;
            GetComponent<AudioSource>().Play();
            Invoke("WinRestart", Delay); //playerControls.timeToRespawn
        }


    }

    void WinRestart()
    {
        SceneManager.LoadScene(0);
    }
}
