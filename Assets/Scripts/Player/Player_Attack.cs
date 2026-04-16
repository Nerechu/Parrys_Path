using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_Attack
{
    private Player_Controller controller;
    private Animator animator;
    private Player_Move moveModule;
    private Player_Parry parryModule;

    [Header("Ataque Normal")]
    public float attackDuration = 0.25f;
    public float attackCooldown = 0.4f;
    public int damageNormal = 1;

    [Header("Contraataque")]
    public float counterAttackDuration = 0.35f;
    public int damageContraataque = 3;

    private bool isAttacking = false;
    private float cooldownTimer = 0f;

    public void Initialize(Player_Controller controller, Animator animator, Player_Move moveModule)
    {
        this.controller = controller;
        this.animator = animator;
        this.moveModule = moveModule;
        this.parryModule = controller.parryModule;
    }

    public void ActualizarAtaque()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (isAttacking) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (parryModule.ventanaContraataque)
                IniciarContraataque();
            else
                IniciarAtaqueNormal();
        }
    }

    void IniciarAtaqueNormal()
    {
        isAttacking = true;
        cooldownTimer = attackCooldown;

        moveModule.movimientoBloqueado = true;
        animator.SetTrigger("Attack");

        controller.CambiarEstado(Player_Controller.EstadoJugador.Atacando);

        controller.StartCoroutine(AtaqueNormalCoroutine());
    }

    IEnumerator AtaqueNormalCoroutine()
    {
        yield return new WaitForSeconds(attackDuration * 0.3f);

        AplicarDaño(damageNormal);

        yield return new WaitForSeconds(attackDuration * 0.7f);

        isAttacking = false;
        moveModule.movimientoBloqueado = false;

        controller.CambiarEstado(Player_Controller.EstadoJugador.Idle);
    }

    void IniciarContraataque()
    {
        isAttacking = true;
        cooldownTimer = attackCooldown;

        moveModule.movimientoBloqueado = true;
        animator.SetTrigger("CounterAttack");

        controller.CambiarEstado(Player_Controller.EstadoJugador.Atacando);

        controller.StartCoroutine(ContraataqueCoroutine());
    }

    IEnumerator ContraataqueCoroutine()
    {
        yield return new WaitForSeconds(counterAttackDuration * 0.3f);

        AplicarDaño(damageContraataque);

        yield return new WaitForSeconds(counterAttackDuration * 0.7f);

        isAttacking = false;
        moveModule.movimientoBloqueado = false;

        controller.CambiarEstado(Player_Controller.EstadoJugador.Idle);
    }

    // -----------------------------
    // APLICAR DAÑO DESDE LA HITBOX
    // -----------------------------
    void AplicarDaño(int daño)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            controller.hitboxAtaque.position,
            controller.radioAtaque,
            controller.capaEnemigos
        );

        foreach (Collider2D hit in hits)
        {
            // ⭐ Buscar SOLO el Enemy_Controller (componente real)
            var enemyController = hit.GetComponentInParent<Enemy_Controller>();

            if (enemyController != null)
            {
                enemyController.healthModule.RecibirDaño(daño);
                Debug.Log("Golpeado: " + hit.name);
            }
        }
    }
}
