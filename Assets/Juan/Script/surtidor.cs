using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class surtidor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cachi;
    bool x;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && x)
        {
            x = false;

            cachi.SetActive(true);
            cachi.GetComponent<cachi>().act = true;



        }
    }
    private void OnTriggerEnter(Collider other)
    {
        x = true;
    }

    private void OnTriggerExit(Collider other)
    {
        x = false;
    }

}
