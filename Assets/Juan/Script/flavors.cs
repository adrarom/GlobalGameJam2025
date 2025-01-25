using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flavors : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Material> sabores = new List<Material>();

    public GameObject cachimba;

    public int X;
    public bool start = false;
    Renderer renderer;
    void Start()
    {
        start = false;
        renderer = cachimba.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && start && cachimba.GetComponent<cachi>().act==true)
        {
            print("bueeee");
            renderer.material = sabores[X];
            cachimba.GetComponent<cachi>().sabor=X;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        start=true;
    }

    private void OnTriggerExit(Collider other)
    {
        start = false;
    }
}