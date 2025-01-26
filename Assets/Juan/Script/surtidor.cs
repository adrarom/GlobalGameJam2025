using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class surtidor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cachi;
    [SerializeField]
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
            FindObjectOfType<TopDownMovement>().isCarryingItem = true;



        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            x = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            x = false;

        }
    }

}
