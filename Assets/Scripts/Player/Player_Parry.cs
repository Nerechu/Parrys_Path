using System.Collections;
using UnityEngine;

[System.Serializable]
public class Player_Parry
{
    private Player_Controller controller;
    private Animator animator;
    private Player_Move moveModule;

    [Header("Parry")]
    public float parryDuration = 0.3f;
    public float parryCooldown = 0.5f;

    // ✔ Necesario para el contraataque
    public bool parryExitoso = false;
    public bool ventanaContraataque = false;
    public float duracionVentanaContraataque = 0.3f;

    public bool isParrying = false;
    private float cooldownTimer = 0f;

    public void Initialize(Player_Controller controller, Animator animator, Player_Move moveModule)
    {
        this.controller = controller;
        this.animator = animator;
        this.moveModule = moveModule;
    }

    public void ActualizarParry()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (isParrying)
            return;

        // ⭐ Tecla K
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Parry: tecla K detectada");
            IniciarParry();
        }
    }

    void IniciarParry()
    {
        Debug.Log("Parry: INICIADO");

        isParrying = true;
        cooldownTimer = parryCooldown;

        moveModule.movimientoBloqueado = true;
        animator.SetTrigger("Parry");

        controller.CambiarEstado(Player_Controller.EstadoJugador.Parrying);

        // ⭐ Avisar al enemigo en frente
        controller.enemyEnFrente?.parryModule.IntentarParry(this);

        controller.StartCoroutine(ParryCoroutine());
    }

    IEnumerator ParryCoroutine()
    {
        // ⭐ Tiempo en el que el player está en modo parry
        yield return new WaitForSeconds(parryDuration);

        Debug.Log("Parry: FINALIZADO");

        // ⭐ Aquí NO ponemos parryExitoso = true automáticamente
        // Ahora depende del rosco y del enemigo
        // Si el enemigo confirma el parry, él mismo activará la animación Parried

        // ⭐ Abrimos ventana de contraataque igualmente
        ventanaContraataque = true;
        controller.StartCoroutine(CerrarVentanaContraataque());

        isParrying = false;
        moveModule.movimientoBloqueado = false;

        controller.CambiarEstado(Player_Controller.EstadoJugador.Idle);
    }

    IEnumerator CerrarVentanaContraataque()
    {
        yield return new WaitForSeconds(duracionVentanaContraataque);

        ventanaContraataque = false;
        parryExitoso = false;
    }
}
