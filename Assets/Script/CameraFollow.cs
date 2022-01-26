using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = false;
    [SerializeField] private Transform target;


    void Update()
    {
        Transform cameraTrans = Camera.main.transform;

        //Follow just X position;
        if (followX && target)
        {
            //new Vector3(Mathf.Lerp(minimum, maximum, t), 0, 0);
            cameraTrans.position = new Vector3(target.transform.position.x, cameraTrans.position.y, cameraTrans.position.z);
        }
    }
}
