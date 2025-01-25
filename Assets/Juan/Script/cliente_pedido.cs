using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class cliente_pedido : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Material> sabores = new List<Material>();

    public int pedido;

    Renderer renderer;
    Renderer renderer_cachi;
    Renderer renderer_cara;



    public GameObject cachi;
    public GameObject cara;
  

    public bool pidiendo;
    bool start;



    public Text losdineros;
    int dinero = 0;
    void Start()
    {
        //StartCoroutine(actitud());
        pidiendo = false;
        renderer = GetComponent<Renderer>();
        renderer_cachi = cachi.GetComponent<Renderer>();
        renderer_cara = cara.GetComponent<Renderer>();
    }

    // Update is called once per frame
    public List<Material> estado = new List<Material>();
    int nivel_actitud;
    float tempo;
    void Update()
    {
        tempo += Time.deltaTime;
        if (tempo > 60)
        {
            fallar();
        }
        if (tempo > 40)
        {
            //print("mal");
            nivel_actitud = 2;
        }
        else if (tempo > 20)
        {
            //print("regular");
            nivel_actitud = 1;

        }
        else
        {
            //print("bien");
            nivel_actitud = 0;

        }
        renderer_cara.material = estado[nivel_actitud];

        if (Input.GetKey(KeyCode.Q))
        {
            pidiendo = false;
        }
        if (!pidiendo)
        {
            pidiendo = true;
            StartCoroutine(pedir());
        }
        if (Input.GetKey(KeyCode.E) && start)
        {
            start = false;

            StartCoroutine(compro());


        }

    }

    IEnumerator pedir()
    {
        tempo = 0;
        nivel_actitud = 0;
        yield return new WaitForSeconds(3f);
        cara.gameObject.SetActive(true);



        pedido = Random.Range(0, 3);

        renderer.material = sabores[pedido];



    }
    IEnumerator compro()
    {
        yield return new WaitForSeconds(0.3f);

        if (cachi.GetComponent<cachi>().sabor == pedido)
        {
            print("si es");
            gameObject.GetComponent<coal>().start = true;
            //StartCoroutine(fumar());
           
        }
        else
        {
            print("nooooooooooooooooooo");
            tempo += 10;
            start = true;

        }
    }

    public IEnumerator fumar()
    {
        cachi.GetComponent<cachi>().sabor = 4;
        cachi.GetComponent<cachi>().act = false;
        cachi.SetActive(false);
        renderer_cachi.material = sabores[3];
        cara.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);

        StartCoroutine(reset());
        acierto();
    }
    IEnumerator reset()
    {

       
        yield return new WaitForSeconds(2f);
        
        renderer.material = sabores[3];

        pedido = 3;


    }

    void romper()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        start = true;
    }

    private void OnTriggerExit(Collider other)
    {
        start = false;
    }

    void acierto()
    {

     dinero =dinero + 100;

      losdineros.text=dinero+"";
}

    void fallar()
    {

        StartCoroutine(reset());
    }






    //------------------------------





}
