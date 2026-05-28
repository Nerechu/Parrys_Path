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

    public void MostrarRoscoNormal()
    {
        gameObject.SetActive(true);
        ventanaActiva = false;
        animator.Play("Rosco_Blanco");
    }

    public void MostrarRoscoParry()
    {
        ventanaActiva = false;
        animator.Play("Rosco_Verde");
    }

    public void OcultarRosco()
    {
        ventanaActiva = false;
        gameObject.SetActive(false);
    }

    public void ParryRosco()
    {
        ventanaActiva = true;
    }
}
