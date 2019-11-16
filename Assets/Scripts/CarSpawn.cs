using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public GameObject Car;

    public float maxTime = 25f;
    public float minTime = 10f;

    private float time;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomTime();
        time = minTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time >= spawnTime)
        {
            SpawnCar();
            SetRandomTime();
        }
    }

    void SpawnCar()
    {
        time = 0;
        Instantiate(Car, transform.position, Car.transform.rotation);
    }

    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }
}
