using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] GameObject playerObject;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObject.transform.position + new Vector3 (0, 0, -10);
        
    }
}
