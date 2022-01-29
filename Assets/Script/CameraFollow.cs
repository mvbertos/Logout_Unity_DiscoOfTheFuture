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

        if (target)
        {
            //Follow just X position;
            if (followX && !followY)
            {
                //new Vector3(Mathf.Lerp(minimum, maximum, t), 0, 0);
                cameraTrans.position = new Vector3(target.transform.position.x, cameraTrans.position.y, cameraTrans.position.z);
            }
            //Follow just X position;
            else if (followX && followY)
            {
                //new Vector3(Mathf.Lerp(minimum, maximum, t), 0, 0);
                cameraTrans.position = new Vector3(target.transform.position.x, target.transform.position.y, cameraTrans.position.z);
            }
        }
    
    }
}
