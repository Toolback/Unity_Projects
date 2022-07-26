using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainPickUp : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clip;

    [SerializeField] int grainScorePoint = 5;

    bool wasCollected = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            FindObjectOfType<GameSession>().UpdatePlayerTotalScore(grainScorePoint, 0);
            gameObject.SetActive(false); // extra safety to avoid double grain pickup bug 
            Destroy(gameObject);


        }
    }
}
