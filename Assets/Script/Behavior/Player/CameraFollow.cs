using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool isFollow;
    public float size;

    public float x, y, z;
    private Vector3 offset;
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    

    [SerializeField] private Transform target;

    // Update is called once per frame
    private void Start()
    {
        offset = new Vector3(x, y, z);
}
    void Update()
    {
        Camera.main.orthographicSize= size;
        if (isFollow)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
