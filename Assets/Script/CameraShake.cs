using System.Threading;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float shake = 0;
    public Transform target;
    [SerializeField] float shakeAmount = 0.7f;
    [SerializeField] float decreaseFactor = 1.0f;


    private void Update()
    {
        if (target)
            Shake(target.position);
    }
    public void Shake(Vector3 position)
    {
        if (shake > 0)
        {
            Vector3 newPos = position + Random.insideUnitSphere * shakeAmount;
            Camera.main.transform.position = new Vector3(newPos.x, newPos.y, -10);

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0.0f;
        }
    }
}
