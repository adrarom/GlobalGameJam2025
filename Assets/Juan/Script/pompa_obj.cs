using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pompa_obj : MonoBehaviour
{
    public Vector2 newPosition;  // La nueva posición a la que quieres mover la imagen
    public RectTransform rectTransform; // Arrastra el RectTransform aquí en el Inspector

    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
         obj = GameObject.Find("pompas");

    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * 500 * Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) // Detecta el clic izquierdo
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            // Lista para almacenar los resultados del raycast
            var raycastResults = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            // Verifica si se ha clickeado en un objeto con raycast
            foreach (var result in raycastResults)
            {

                if (result.gameObject.name == "pompa(Clone)")
                {
                    Destroy(result.gameObject);
                    obj.GetComponent<pompas>().puntuacion++;
                }
            }
        }
    }

}
