
using UnityEngine;
using UnityEngine.UI;

public class RotateWithDrag : MonoBehaviour
{
    
    private float rotateSpeedModifier = 1f;    
    private Vector3 screenPos;
    private float angleOffset;  

    [SerializeField] GameObject cube;

    /****************************************************************************************************************/
    //  Began:	        A finger touched the screen.
    //  Moved:	        A finger moved on the screen.
    //  Stationary:	A finger is touching the screen but hasn't moved.
    //  Ended:	        A finger was lifted from the screen. This is the final phase of a touch.
    //  Canceled:	    The system cancelled tracking for the touch.      


    // Se encarga de rotar un objeto (en este caso, un cubo) de forma continua hacia la izquierda o derecha en función de la dirección del ratón
    void ContinuousRotation()
    {
        float rotationSpeed = 2;
        // verifica si se ha hecho clic en el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            //almacena la posición actual del cubo en la variable screenPos
            screenPos = cube.transform.position;
            //calcula la diferencia entre la posición del mouse y la posición del cubo en la pantalla. Esto nos dará la dirección en la que se debe rotar el cubo.
            /*La función WorldToScreenPoint convierte una posición en el espacio del mundo a una posición en la pantalla. Esto es necesario para calcular la diferencia entre la posición del mouse y la posición del cubo en la pantalla, lo que nos da la dirección en la que se debe rotar el cubo.*/
            Vector3 v3 = Input.mousePosition - Camera.main.WorldToScreenPoint(screenPos);
            //calcula el ángulo de rotación inicial del cubo. Utiliza la función Atan2 para calcular el ángulo entre el vector de dirección del cubo y el vector de dirección del mouse.
            angleOffset = (Mathf.Atan2(cube.transform.right.y, cube.transform.right.x) - Mathf.Atan2(v3.y, v3.x)) * Mathf.Rad2Deg;
        }
        // verifica si el botón izquierdo del mouse sigue presionado
        if (Input.GetMouseButton(0))
        {
            // calcula nuevamente la diferencia entre la posición del mouse y la posición del cubo en la pantalla. Esto nos dará la dirección en la que se debe rotar el cubo.
            Vector3 v3 = Input.mousePosition - Camera.main.WorldToScreenPoint(screenPos);
            // calcula el ángulo de rotación actual del cubo. Utiliza la función Atan2 para calcular el ángulo entre el vector de dirección del cubo y el vector de dirección del mouse.
            float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
            // establece la rotación del cubo en el ángulo calculado más el ángulo de compensación inicial. Esto asegura que el cubo gire desde el punto donde se hizo clic inicialmente.
            // Para modificar la velocidad de rotación, puedes multiplicar el ángulo de rotación por un factor de escala
            cube.transform.eulerAngles = new Vector3(0, 0, angle + angleOffset) * rotationSpeed;
        }
    }

    void RotateCubeHorizontally()
    {
        if (cube == null || Input.touchCount != 1) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            float rotZ = touch.deltaPosition.x * rotateSpeedModifier; // Movimiento horizontal del dedo
            cube.transform.Rotate(0f, 0f, rotZ, Space.World); // Rota en el eje Z
        }
    }

    void RotateCubeVertically()
    {
        if (cube == null || Input.touchCount != 1) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            float rotX = -touch.deltaPosition.y * rotateSpeedModifier; // Movimiento vertical del dedo
            cube.transform.Rotate(rotX, 0f, 0f, Space.World); // Rota en el eje X
        }
    }




    private void Update()
    {
        //ContinuousRotation();
        RotateCubeHorizontally();
        //RotateCubeVertically();



      


    }

    

    
}


