using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{

    [SerializeField] GameObject SubjectToFollow;

    void LateUpdate()
    {
        transform.position = SubjectToFollow.transform.position + new Vector3 (0, 0, -10);

    }
}
