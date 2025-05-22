using UnityEngine;

public class ControladorDeBaileTeletransporte : MonoBehaviour
{
    public Animator animator;  

    // Método para teletransportar al personaje y activar el baile
    public void TeletransportarYBailar(Vector3 destino, int idBaile)
    {
        
        transform.position = destino;

 
        if (animator != null)
        {
            animator.SetInteger("IdBaile", idBaile);
        }
        else
        {
            Debug.LogWarning("Animator no asignado.");
        }
    }
}


