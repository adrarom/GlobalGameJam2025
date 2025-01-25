using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BasketballThrow : MonoBehaviour
{
    // Referencias a objetos de la escena

    public GameObject panel;
    public Slider powerSlider;         // Barra de poder

    float powerLevel;              // Nivel de poder calculado

    public float min;              
    public float max;              

    private bool isChargingPower = false;  // Indica si se está cargando poder

    bool x = true;
    public bool start = false;



    // Para mostrar el tiempo en la UI
    public Text textoTemporizador;




    private void Start()
    {
        panel.SetActive(false);
        powerLevel = 0;
        x = true;
        
      

    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.E) && x)
        {
            x = false;
          start = true;

        }
        // Inicia la carga de poder cuando se mantiene la barra espaciadora
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isChargingPower && start) // Evita reiniciar la corrutina varias veces
            {
                StopCoroutine("ChargePower");

                StartCoroutine(Comprobar());
                isChargingPower = true;
               // StartCoroutine("ChargePower");
            }
        }
        else
        {
            if (isChargingPower)
            {
                isChargingPower = false;
           

            }
        }
        if (start)
        {
            start = false;
            panel.SetActive(true);
            StartCoroutine("ChargePower");
            
        }

       


    }






    IEnumerator ChargePower()
    {
       

        bool decreasingPower = false;

        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            if (powerLevel >= 100) 
            {
                decreasingPower = true; 
            }
            if (powerLevel <= 0)
            {
                decreasingPower = false;
            }

            if (!decreasingPower)
            {
                powerLevel += 1f;
            }
            else
            {
                powerLevel -= 1f;
            }
            powerSlider.value = powerLevel;
        }
    }
    IEnumerator temppo()
    {
        yield return new WaitForSeconds(5f);
        fin_bad();

    }
    IEnumerator Comprobar()
    {
        if(powerLevel<max && powerLevel > min)
        {
            yield return new WaitForSeconds(1f);

            fin();
        }
        else
        {
            yield return new WaitForSeconds(0.01f);

            fin_bad();
        }
        yield return new WaitForSeconds(0.01f);

    }
    private void fin()
    {

        print(":)");
        panel.SetActive(false);
        powerLevel = 0;
        x = true;
      


    }
    private void fin_bad()
    {
        print(":(");
        panel.SetActive(false);
        powerLevel = 0;
        x = true;



    }
}


  



