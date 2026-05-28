using UnityEngine;

[System.Serializable]
public class Enemy_Attack
{
    private Enemy_Controller controller;
    private Animator animator;
    private Transform enemyTransform;

    [Header("Ataque")]
    public float attackRange = 0.6f;
    public float attackCooldown = 1f;
    public int damage = 1;

    private float cooldownTimer = 0f;

    public void Initialize(Enemy_Controller controller, Animator animator, Transform transform)
    {
        this.controller = controller;
        this.animator = animator;
        this.enemyTransform = transform;
    }

    public void ActualizarAtaque()
    {
        if (controller.estadoActual == Enemy_Controller.EstadoEnemigo.Muerto)
            return;

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        Collider2D hit = Physics2D.OverlapCircle(
            enemyTransform.position,
            attackRange,
            LayerMask.GetMask("Player")
        );

        if (hit != null)
        {
            controller.CambiarEstado(Enemy_Controller.EstadoEnemigo.Atacando);

            controller.parryModule.MostrarRoscoNormal();

            animator.SetTrigger("Attack");

            cooldownTimer = attackCooldown;
        }
    }

    public void ActivarVentanaParry()
    {
        controller.parryModule.MostrarRoscoParry();
    }

    public void OcultarRosco()
    {
        controller.parryModule.OcultarRosco();

        controller.CambiarEstado(Enemy_Controller.EstadoEnemigo.Idle);
    }
}
