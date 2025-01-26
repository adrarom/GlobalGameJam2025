using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pompas : MonoBehaviour
{

    public GameObject prefab;  // El prefab que quieres instanciar
    public GameObject spawn1, spawn2, spawn3, spawn4;
    public GameObject panel;

    public int puntuacion=0;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(spawn());

    }

    // Update is called once per frame
    void Update()
    {
        if (puntuacion >= 10)
        {
            print("ganas");
        }
        
    }
    IEnumerator spawn()
    {
        while (true)
        {

        yield return new WaitForSeconds(0.5f);
        int pedido = Random.Range(0, 4);
        if (pedido == 0)
        {
            GameObject spawnedPrefab = Instantiate(prefab, spawn1.transform.position, Quaternion.identity);
            spawnedPrefab.transform.SetParent(panel.transform);
        }
        if (pedido == 1)
        {
            GameObject spawnedPrefab = Instantiate(prefab, spawn2.transform.position, Quaternion.identity);
            spawnedPrefab.transform.SetParent(panel.transform);
        }
        if (pedido == 2)
        {
            GameObject spawnedPrefab = Instantiate(prefab, spawn3.transform.position, Quaternion.identity);
            spawnedPrefab.transform.SetParent(panel.transform);
        }
        if (pedido == 3)
        {
            GameObject spawnedPrefab = Instantiate(prefab, spawn4.transform.position, Quaternion.identity);
            spawnedPrefab.transform.SetParent(panel.transform);
        }
        }

    }
}
