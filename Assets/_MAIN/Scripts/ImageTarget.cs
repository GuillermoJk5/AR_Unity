using UnityEngine;
using Vuforia;

public class ImageTargetTriggerVuforia10 : MonoBehaviour
{
    public ControladorDeBaileTeletransporte controlador; // Tu personaje
    public float distanciaFrontal = 0.5f;                 // Distancia delante del target
    public int idBaile = 1;                               // ID del baile

    private ObserverBehaviour observerBehaviour;
    private Camera arCamera;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogWarning("No se encontró la cámara principal (MainCamera) para mirar.");
        }

        // Al inicio, ocultamos el personaje para que no aparezca antes de detectar
        if (controlador != null)
            controlador.gameObject.SetActive(false);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            Vector3 posicionFrontal = transform.position + transform.forward * distanciaFrontal;

            controlador.TeletransportarYBailar(posicionFrontal, idBaile);

            if (arCamera != null)
            {
                Vector3 direccionCamara = arCamera.transform.position - controlador.transform.position;
                direccionCamara.y = 0;

                if (direccionCamara != Vector3.zero)
                {
                    Quaternion rotacionMirar = Quaternion.LookRotation(direccionCamara);
                    Quaternion rotacionFinal = rotacionMirar * Quaternion.Euler(-90f, 180f, 180f);
                    controlador.transform.rotation = rotacionFinal;
                }
            }

            controlador.gameObject.SetActive(true);
        }
        else
        {
            controlador.gameObject.SetActive(false);
        }
    }
}
