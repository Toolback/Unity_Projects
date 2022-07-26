using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryQuest : MonoBehaviour
{
    public bool hasWin = false;
    public bool passengerIsAlive = true;
    public bool hasPickUpPassenger = false;
    public bool hasPickedUpPackage = false;
    Color32 hasMurder = new Color32(1, 1, 1, 1); // Return to normal color after X sec ? 
    Color32 baseColor;

    SpriteRenderer spriteRenderer;
    GameObject PickupPZone;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        PickupPZone = GameObject.Find("PickUpPassengerZone");
    }

    // Collide from Collider Zone
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If Passenger Get Touch, He dead 
        if(collision.collider.tag == "Passenger")
        {
            passengerIsAlive = false;
            hasPickUpPassenger = false;
            Destroy(GameObject.Find("PickUpPassengerZone"));

            // Body disapear after 15sec
            StartCoroutine(waiter(15f));
            Destroy(collision.gameObject);
        }


        passengerIsAlive = false;
    }


    // Trigger from Collider Zone 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pick Up Passenger 
        if(collision.tag == "PassengerPickUpZone" && passengerIsAlive)
        {
            StartCoroutine(waiter(20f));
            // If Passenger still alive after X sec, get him on board
            if (passengerIsAlive)
            {
                Destroy(collision.gameObject.transform.parent.gameObject);

                hasPickUpPassenger = true;
            }
            
            
        }

        if(collision.tag == "Package")
        {
            Destroy(collision.gameObject);
            hasPickedUpPackage = true;
        }

        if(collision.tag == "EndLine" && hasPickedUpPackage && hasPickUpPassenger)
        {
            hasWin = true;
        }

        if(collision.tag == "SpeedUp")
        {

        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    IEnumerator waiter( float timetoWait)
    {
        yield return new WaitForSecondsRealtime(timetoWait);
    }
}   
