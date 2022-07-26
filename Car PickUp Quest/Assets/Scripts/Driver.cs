using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
     [SerializeField] float mvtSpeed = 20f;
    [SerializeField] float turnSpeed = 300f;
    [SerializeField] float boostSpeed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        float mvtAmount = Input.GetAxis("Vertical") * ( mvtSpeed + boostSpeed ) * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, mvtAmount, 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SpeedUp")
        {
            boostSpeed = boostSpeed + 50f;
        }
    }
}
