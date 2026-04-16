using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_Move
{
    [Header("Movimiento")]
    public float speed = 5f;
    private Rigidbody2D playerRb;
    private Vector2 direccion;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;
    public bool isInvulnerable = false;

    private bool isDashing = false;
    private float dashTime;
    private float dashCooldownTimer;

    // 🔥 NUEVO: para bloquear movimiento desde el parry
    public bool movimientoBloqueado = false;

    private Animator animator;
    private Player_Controller controller;
    private Transform transform;

    public void Initialize(Player_Controller controller, Animator animator, Rigidbody2D rb, Transform transform)
    {
        this.controller = controller;
        this.animator = animator;
        this.playerRb = rb;
        this.transform = transform;
    }

    public void ActualizarMovimiento()
    {
        // 🔥 Si está bloqueado, no hacemos nada
        if (movimientoBloqueado) return;

        // Cooldown del dash
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // Si estamos dashing, no leemos el input
        if (isDashing) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Animaciones
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);

        if (moveX != 0 || moveY != 0)
        {
            animator.SetFloat("UltimoX", moveX);
            animator.SetFloat("UltimoY", moveY);
            controller.CambiarEstado(Player_Controller.EstadoJugador.Moviendo);
        }
        else
        {
            controller.CambiarEstado(Player_Controller.EstadoJugador.Idle);
        }

        direccion = new Vector2(moveX, moveY).normalized;

        // Activar dash
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
        {
            StartDash();
        }
    }

    public void ActualizarMovimientoFisico()
    {
        // 🔥 Si está bloqueado, no movemos al jugador
        if (movimientoBloqueado) return;

        if (isDashing)
        {
            playerRb.MovePosition(playerRb.position + direccion * dashSpeed * Time.fixedDeltaTime);
            return;
        }

        playerRb.MovePosition(playerRb.position + direccion * speed * Time.fixedDeltaTime);
    }

    void StartDash()
    {
        isDashing = true;
        isInvulnerable = true;

        dashTime = dashDuration;
        dashCooldownTimer = dashCooldown;

        // Si no hay input, se hace el dash hacia la última dirección
        if (direccion == Vector2.zero)
        {
            direccion = new Vector2(
                animator.GetFloat("UltimoX"),
                animator.GetFloat("UltimoY")
            ).normalized;
        }

        controller.StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        while (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        isInvulnerable = false;
    }
}
