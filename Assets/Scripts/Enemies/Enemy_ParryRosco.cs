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
        animator.Play("ParryRosco_Animation");
    }

    public void MostrarRoscoParry()
    {
        ventanaActiva = false;
        animator.Play("ParryRoscoGreen_Animation");
    }

    public void OcultarRosco()
    {
        ventanaActiva = false;
        gameObject.SetActive(false);
    }

    // ⭐ Animation Event en ParryRoscoGreen_Animation
    public void ParryRosco()
    {
        ventanaActiva = true;
    }
}
