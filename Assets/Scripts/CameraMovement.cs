using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CubeSpawner spawner;

    public Vector3 initPos;
    public float interval;
    private float startTime;
    private Vector3 startPos;

    void Start()
    {
        startTime = Time.time;
        startPos = transform.position;
    }
    
    void Update()
    {
        float deltaTime = Time.time - startTime;
        Vector3 disp = (Vector3.up + Vector3.back) * 
            spawner.cubeCount() / 10f;
        transform.position = Vector3.Slerp(
            startPos, initPos + disp, deltaTime / interval);
        if (deltaTime > interval)
            Reset();
    }

    public void Reset()
    {
        startTime = Time.time;
        startPos = transform.position;
    }
}
