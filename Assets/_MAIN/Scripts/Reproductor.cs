using UnityEngine;
using UnityEngine.UI;

public class ReproductorMultiple : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] pistas;
    private int indiceActual = 0;

    [Header("UI Play/Pause")]
    public Button botonPlayPause;
    public Sprite iconoPlay;
    public Sprite iconoPause;
    private Image imagenBoton;

    void Start()
    {
        if (pistas.Length > 0)
        {
            audioSource.clip = pistas[indiceActual];
        }

        if (botonPlayPause != null)
        {
            imagenBoton = botonPlayPause.GetComponent<Image>();
            botonPlayPause.onClick.AddListener(ToggleReproduccion);
            ActualizarIcono();
        }
    }

    public void ReproducirPista(int indice)
    {
        if (indice >= 0 && indice < pistas.Length)
        {
            indiceActual = indice;
            audioSource.clip = pistas[indiceActual];
            audioSource.Play();
            ActualizarIcono();
        }
    }

    public void Pausar()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            ActualizarIcono();
        }
    }

    public void Continuar()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            ActualizarIcono();
        }
    }

    public void Detener()
    {
        audioSource.Stop();
        ActualizarIcono();
    }

    public void ToggleReproduccion()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }

        ActualizarIcono();
    }

    private void ActualizarIcono()
    {
        if (imagenBoton == null) return;

        if (audioSource.isPlaying)
        {
            imagenBoton.sprite = iconoPause;
        }
        else
        {
            imagenBoton.sprite = iconoPlay;
        }
    }
}
