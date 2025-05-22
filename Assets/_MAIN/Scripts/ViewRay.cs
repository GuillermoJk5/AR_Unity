
using UnityEngine;
using TMPro;

public class ViewRay : MonoBehaviour
{
  
	[SerializeField] GameObject ARcam; // hace referencia a la cámara AR (probablemente la cámara principal del dispositivo).
    [SerializeField] float range = 5f; // la longitud del rayo dibujado en la escena para depuración visual.
    private string infoName; //  almacena temporalmente el nombre del objeto que ha sido tocado.
    [SerializeField] TextMeshProUGUI txtInfo; //  UI de texto donde se muestra el nombre del objeto tocado.

    /****************************************************************************************************************/
    //  Began:	        A finger touched the screen.
    //  Moved:	        A finger moved on the screen.
    //  Stationary:	A finger is touching the screen but hasn't moved.
    //  Ended:	        A finger was lifted from the screen. This is the final phase of a touch.
    //  Canceled:	    The system cancelled tracking for the touch.      


    void Update()
	{
		Vector3 lineOrigin = ARcam.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); // Se calcula el punto central de la vista de la cámara ((0.5f, 0.5f)) en coordenadas del mundo.

        Debug.DrawRay(lineOrigin, ARcam.transform.forward * range, Color.green);

        CheckClickWithRay(); // Se llama al método CheckClickWithRay() en cada frame para detectar toques.

    }
   
    void CheckClickWithRay()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //Detecta si se ha tocado la pantalla y si el toque acaba de comenzar.
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            // Se lanza un raycast desde el punto tocado en pantalla al mundo 3D.
            if (Physics.Raycast(ray, out hit)) // Si el rayo colisiona con algún objeto que tenga un collider, se ejecuta el siguiente bloque.
            {
                Debug.Log(hit.transform.name);              
                infoName = hit.transform.name;
                txtInfo.text = infoName;
                // Se guarda y muestra el nombre del objeto tocado.
                switch (infoName)
                { // Si el nombre del objeto es Rojo, Azul o Amarillo, se cambia el color de su material correspondiente
                    case "Rojo":
                    hit.transform.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.red;
                    break;
                    case "Azul":
                        hit.transform.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
                        break;
                    case "Amarillo":
                        hit.transform.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
                        break;

                }
            }
        }
    }
}

