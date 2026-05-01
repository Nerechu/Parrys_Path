using UnityEngine;

public class Enemy_ParryRosco : MonoBehaviour
{
    private Animator animator;

    [Header("Ventana de parry")]
    public bool ventanaActiva = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    // ⭐ Mostrar rosco blanco
    public void MostrarRoscoNormal()
    {
        gameObject.SetActive(true);
        ventanaActiva = false;
        animator.Play("Rosco_Blanco");
    }

    // ⭐ Mostrar rosco verde
    public void MostrarRoscoParry()
    {
        ventanaActiva = false;
        animator.Play("Rosco_Verde");
    }

    // ⭐ Ocultar rosco (cierra ventana)
    public void OcultarRosco()
    {
        ventanaActiva = false;
        gameObject.SetActive(false);
    }

    // ⭐ Evento al inicio de la animación verde
    public void ParryRosco()
    {
        ventanaActiva = true;
    }
}
