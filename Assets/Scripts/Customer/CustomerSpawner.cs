using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnInterval = 5f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnCustomer();
            timer = spawnInterval;
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, transform.position, transform.rotation);
    }
}
