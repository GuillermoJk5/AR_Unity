
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    [SerializeField] private Camera arCamera; // La cámara AR que lanza el raycast
    private float initialDistance;
    private Vector3 initialScale;

    [SerializeField] Button btZoom;
    [SerializeField] GameObject cube;
    int countClicks = 1;

    /****************************************************************************************************************/
    //  Began:	        A finger touched the screen.
    //  Moved:	        A finger moved on the screen.
    //  Stationary:	A finger is touching the screen but hasn't moved.
    //  Ended:	        A finger was lifted from the screen. This is the final phase of a touch.
    //  Canceled:	    The system cancelled tracking for the touch.      

    private void Start()
    {
        initialScale = cube.transform.localScale;

        // => expresión que devuelve un método con o sin parámetros
        btZoom.onClick.AddListener(() => Test());

    }

    /*Método que llamará el botón que tengamos asignado en el editor como "btZoom" y modificará la escala
     de "cube" , le podéis pasar el GameObject que queráis
     */
    void Test()
    {
        countClicks++;
        print(countClicks);

        cube.transform.localScale = initialScale * countClicks;
    }

    void Update()
    {
        DetectSelection(); // Tocar para seleccionar el cubo
        ScaleSelectedObjectWithPinch(); // Hacer zoom si hay selección

    }

    /*escalar usando "pinch"(separar el dedo y el pulgar o juntarlos en la pantalla para acercar o alejar una imagen, activar una función, etc), implica dos toques. Necesitamos contar ambos toques, almacenarlos, medir la distancia entre los dedos y escalar el GameObject dependiendo de la distancia. También debemos ignorar si la distancia entre los dedos es pequeña (casos en los que se registran dos toques accidentalmente)*/

    void DetectSelection()   // Detectar el toque sobre el cubo.
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Cubo")) // El cubo tiene que tener el tag Cubo
                {
                    cube = hit.transform.gameObject;  // Guardar una referencia al cubo seleccionado.
                }
                else
                {
                    cube = null;
                }
            }
        }
    }

    void ScaleSelectedObjectWithPinch()
    {
        if (cube == null) return; // // Permitir el zoom solo si hay un cubo seleccionado.

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                if (initialDistance < 10f) return;
                initialScale = cube.transform.localScale;
            }
            else
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                if (initialDistance < 10f) return;

                float factor = currentDistance / initialDistance;
                Vector3 newScale = initialScale * factor;

                // Limitar escala
                newScale = Vector3.Max(newScale, Vector3.one * 0.05f);
                newScale = Vector3.Min(newScale, Vector3.one * 0.5f);

                cube.transform.localScale = newScale;
            }
        }

    }
}