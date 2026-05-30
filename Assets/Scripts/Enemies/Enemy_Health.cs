using System.Collections;
using UnityEngine;

[System.Serializable]
public class Enemy_Health
{
    private Enemy_Controller controllerMele;
    private Animator animator;

    [Header("Vida del enemigo")]
    public int vidaMaxima = 5;
    public int vidaActual = 5;

    [Header("Muerte")]
    public bool destruirAlMorir = true;
    public float tiempoAntesDeDestruir = 0.5f;

    public void Initialize(Enemy_Controller controllerMele, Animator animator)
    {
        this.controllerMele = controllerMele;
        this.animator = animator;
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        Debug.Log("Enemigo recibe daño: " + cantidad + " | Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
        else
        {
            if (animator != null)
                animator.SetTrigger("Hit");
        }
    }

    void Morir()
    {
        Debug.Log("Enemigo muerto");

        if (animator != null)
            animator.SetTrigger("Death");

        if (controllerMele != null)
        {
            controllerMele.CambiarEstado(Enemy_Controller.EstadoEnemigo.Muerto);

            if (destruirAlMorir)
                controllerMele.StartCoroutine(DestruirDespues(controllerMele.gameObject));
        }
    }

    IEnumerator DestruirDespues(GameObject go)
    {
        yield return new WaitForSeconds(tiempoAntesDeDestruir);
        GameObject.Destroy(go);
    }
}
