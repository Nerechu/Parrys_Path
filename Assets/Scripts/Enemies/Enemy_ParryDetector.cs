using System.Collections;
using UnityEngine;

public class Enemy_ParryDetector : MonoBehaviour
{
    private Enemy_Controller controller;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Parry")]
    public float stunDuration = 0.4f;

    [Header("Rosco")]
    public Enemy_ParryRosco rosco;

    private bool isParried = false;

    public void Initialize(Enemy_Controller controller, Animator animator, Rigidbody2D rb)
    {
        this.controller = controller;
        this.animator = animator;
        this.rb = rb;

        if (rosco != null)
            rosco.gameObject.SetActive(false);
    }

    public void MostrarRoscoNormal()
    {
        rosco?.MostrarRoscoNormal();
    }

    public void MostrarRoscoParry()
    {
        rosco?.MostrarRoscoParry();
    }

    public void OcultarRosco()
    {
        rosco?.OcultarRosco();
    }

    public void IntentarParry(Player_Parry playerParry)
    {
        if (isParried)
            return;

        if (!rosco.ventanaActiva)
            return;

        if (!playerParry.isParrying)
            return;

        controller.StartCoroutine(ParriedCoroutine());
    }

    IEnumerator ParriedCoroutine()
    {
        isParried = true;

        rb.linearVelocity = Vector2.zero;
        controller.CambiarEstado(Enemy_Controller.EstadoEnemigo.Parried);

        animator.SetTrigger("Parried");

        OcultarRosco();

        yield return new WaitForSeconds(stunDuration);

        controller.CambiarEstado(Enemy_Controller.EstadoEnemigo.Idle);
        isParried = false;
    }
}
