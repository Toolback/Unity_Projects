using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour

{
    [SerializeField] public bool hasWinThisGame = false;
    [SerializeField] public int WinCount = 0;
    [SerializeField] public int GameCount = 0;
    [SerializeField] public float timeToRespawn = 1f;
    [SerializeField] public float torqueAmount = 1f;
    [SerializeField] public float boostSpeed = 80f;
    [SerializeField] public float baseSpeed = 20f;



    Rigidbody2D rb2d;
    SurfaceEffector2D effector;

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // Find First type of name -- (better be only one, or designed to)
        effector = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }

    }

    public void DisableControls()
    {
        canMove = false;
    }

    private void RespondToBoost()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            effector.speed = boostSpeed;
        }
        else 
        {
            effector.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        // else if avoid double left and right push at same time
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}
