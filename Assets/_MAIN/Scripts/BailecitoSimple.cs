using UnityEngine;

public class ControladorDeBaile : MonoBehaviour
{
    public Animator animator; // Asigna tu Animator en el Inspector

    // Cambia el valor de la variable "IdBaile" en el Animator
    public void CambiarIdBaile(int nuevoId)
    {
        if (animator != null)
        {
            animator.SetInteger("IdBaile", nuevoId);
        }
        else
        {
            Debug.LogWarning("Animator no asignado en " + gameObject.name);
        }
    }
}
