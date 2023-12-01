using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject target;

    public float CameraSpeed = 10.0f;
    Vector3 targetPos;

    void FixedUpdate()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        //targetPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 10);

        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * CameraSpeed);
    }
}
