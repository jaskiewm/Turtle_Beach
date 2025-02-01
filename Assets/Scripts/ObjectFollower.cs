using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    public Vector3 newOffset;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.position.z < 160)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else if(target != null && target.position.z > 160)
        {
            Vector3 targetPosition = target.position + newOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
