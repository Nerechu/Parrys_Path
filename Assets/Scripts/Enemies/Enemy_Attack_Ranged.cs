using UnityEngine;

[System.Serializable]
public class Enemy_Attack_Ranged
{
    private Enemy_Controller_Ranged controller;
    private Animator animator;
    private Transform player;

    [Header("Ataque")]
    public float distanciaAtaque = 5f;
    public float cooldown = 2f;

    private float tiempoUltimoAtaque;

    public void Initialize(Enemy_Controller_Ranged controller, Animator animator, Transform player)
    {
        this.controller = controller;
        this.animator = animator;
        this.player = player;
    }

    public void ActualizarAtaque()
    {
        if (player == null)
            return;

        float dist = Vector2.Distance(player.position, animator.transform.position);

        if (dist > distanciaAtaque)
            return;

        if (Time.time < tiempoUltimoAtaque + cooldown)
            return;

        // ⭐ Estado Atacando
        controller.CambiarEstado(Enemy_Controller_Ranged.EstadoEnemigo.Atacando);
        animator.SetTrigger("Attack");

        tiempoUltimoAtaque = Time.time;

        // ⭐ Estado Cooldown
        controller.CambiarEstado(Enemy_Controller_Ranged.EstadoEnemigo.Cooldown);
    }
}
