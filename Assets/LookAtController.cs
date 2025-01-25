using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtController : MonoBehaviour
{
    public GameObject mainController;
    public float offsetX, offsetY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position = mainController.transform.position;
        Vector3 newPosition = new Vector3(mainController.transform.position.x + offsetX, mainController.transform.position.y, mainController.transform.position.z + offsetY);
    }
}
