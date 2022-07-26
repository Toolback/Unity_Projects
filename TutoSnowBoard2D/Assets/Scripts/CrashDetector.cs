using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{

    PlayerControls playerControls;
    [SerializeField] ParticleSystem LoseEffect;
    [SerializeField] AudioClip crashSFX;

    bool hasCrashed = false;


    private void Start()
    {
        playerControls = GetComponent<PlayerControls>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // If Player's head hit the ground then restart the game at scene 0 after 2sec
        if(collision.tag == "Ground" && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerControls>().DisableControls();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            LoseEffect.Play();
            playerControls.GameCount += 1;
            Invoke("Restarter", playerControls.timeToRespawn);
        }
        
        
    }

    void Restarter()
    {
        SceneManager.LoadScene(0);
    }
}
