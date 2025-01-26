using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class cliente_pedido : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Material> sabores = new List<Material>();
    public Customer customer;
    public int pedido;
    bool isPlayerOnTrigger;
    Renderer renderer;
    Renderer renderer_cachi;
    Renderer renderer_cara;

    public GameObject cachi;
    public GameObject cara;

    public bool pidiendo;
    bool start = true;

    public TextMeshProUGUI losdineros;
    int dinero = 0;

    void Start()
    {
        //StartCoroutine(actitud());
        pidiendo = false;
        renderer = GetComponent<Renderer>();
        renderer_cachi = cachi.GetComponent<Renderer>();
        renderer_cara = cara.GetComponent<Renderer>();
        cara.SetActive(false);
    }

    // Update is called once per frame
    public List<Material> estado = new List<Material>();
    int nivel_actitud;
    float tempo;

    void Update()
    {
        tempo += Time.deltaTime;
        if (tempo > 45)
        {
            fallar();
        }
        if (tempo > 30)
        {
            //print("mal");
            nivel_actitud = 2;
        }
        else if (tempo > 15)
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
        if (Input.GetKey(KeyCode.E) && start && isPlayerOnTrigger)
        {
            start = false;
            StartCoroutine(compro());
        }
    }

    IEnumerator pedir(Customer customer)
    {
        yield return new WaitForSeconds(3f);
        string requestedFlavor = customer.requestedFlavor;
        Debug.Log(customer);
        Debug.Log("Pedir");
        tempo = 0;
        nivel_actitud = 0;
        yield return new WaitForSeconds(3f);
        cara.gameObject.SetActive(true);

        switch (requestedFlavor)
        {
            case "Fresa":
                pedido = 0;
                break;
            case "Platano":
                pedido = 1;
                break;
            case "Menta":
                pedido = 2;
                break;
            default:
                Debug.LogError("Sabor no reconocido: " + requestedFlavor);
                break;
        }
        renderer.material = sabores[pedido];
    }

    IEnumerator compro()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log(cachi.GetComponent<cachi>().sabor);
        Debug.Log(pedido);
        if (cachi.GetComponent<cachi>().sabor == pedido)
        {
            print("si es");
            coal coal = gameObject.GetComponent<coal>();
            coal.start = true;
            coal.pedido = pedido;
            //StartCoroutine(fumar());
        }
        else
        {
            print("nooooooooooooooooooo");
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
    }

    void romper()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && !pidiendo)
        {
            cara.SetActive(true);
            Customer customer = other.gameObject.GetComponent<Customer>();
            Debug.Log(customer.gameObject);
            pidiendo = true;
            StartCoroutine(pedir(customer));
        }
        if (other.CompareTag("Player"))
        {
            isPlayerOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            pidiendo = false;
        }
        if (other.CompareTag("Player"))
        {
            isPlayerOnTrigger = false;
        }
    }

    void acierto()
    {
        dinero = dinero + 100;
        losdineros.text = dinero + "";
    }

    void fallar()
    {

    }
}
