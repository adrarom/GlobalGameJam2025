using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public bool estado = false;
    public GameObject panel;
    // Start is called before the first frame update
  

    public void jugar()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void how()
    {
    
        if (!estado)
        {
            estado = true;
        panel.SetActive (true);

        }
        else if(estado){
            estado = false;
            panel.SetActive(false);
        }
    }
    public void exit()
    {
        Application.Quit();

    }
}
