using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject normalCube;
    public GameObject lightCube;
    public Transform[] cubeSpawns;

    public float chance;
    public float interval;
    private float spawnTime;

    private List<GameObject> cubes;

    void Awake()
    {
        cubes = new List<GameObject>();
    }

    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn(Random.Range(0f, 1f) < chance);
            spawnTime = Time.time + interval;
        }
    }

    void Spawn(int index, bool isNormal)
    {
        GameObject original = isNormal ? 
            normalCube : lightCube;
        GameObject cube = Instantiate(original, 
            cubeSpawns[index].position, Quaternion.identity);
        cubes.Add(cube);
    }

    void Spawn(bool isNormal)
    {
        float rand = RandGaussian(
            (cubeSpawns.Length - 1) / 2f);
        int index = Mathf.Clamp(
            (int)rand, 0, cubeSpawns.Length - 1);
        Spawn(index, isNormal);
    }

    float RandGaussian(float mean, float stdDev = 1f)
    {
        float u1 = Random.Range(0f, 1f), 
            u2 = Random.Range(0f, 1f);
        float randStdNormal = 
            Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Sin(2f * Mathf.PI * u2);
        float randNormal = 
            mean + stdDev * randStdNormal;
        return randNormal;
    }

    public int cubeCount()
    {
        for (int i = 0; i < cubes.Count; ++i)
        {
            if (cubes[i] == null)
            {
                cubes.RemoveAt(i);
                --i;
            }
        }
        return cubes.Count;
    }
}
