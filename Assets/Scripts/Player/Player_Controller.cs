using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public enum EstadoJugador
    {
        Idle,
        Moviendo,
        Parrying,
        Atacando,
        Muerto
    }

    [Header("Estado actual")]
    public EstadoJugador estadoActual = EstadoJugador.Idle;

    [Header("Módulos del jugador")]
    public Player_Move moveModule;
    public Player_Parry parryModule;
    public Player_Attack attackModule;   // ⭐ AÑADIDO

    [Header("Ataque del jugador")]
    public Transform hitboxAtaque;
    public float radioAtaque = 0.6f;
    public LayerMask capaEnemigos;

    [Header("Detección de enemigo para parry")]
    public float distanciaParry = 1.2f;
    public float offsetParry = 0.6f;
    public Enemy_Controller enemyEnFrente;

    private Animator animator;
    private Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (moveModule != null)
            moveModule.Initialize(this, animator, rb, transform);

        if (parryModule != null)
            parryModule.Initialize(this, animator, moveModule);

        if (attackModule != null)                      // ⭐ AÑADIDO
            attackModule.Initialize(this, animator, moveModule);
    }

    void Update()
    {
        if (estadoActual == EstadoJugador.Muerto)
            return;

        DetectarEnemigoEnFrente();

        moveModule?.ActualizarMovimiento();
        parryModule?.ActualizarParry();
        attackModule?.ActualizarAtaque();              // ⭐ AÑADIDO
    }

    void FixedUpdate()
    {
        moveModule?.ActualizarMovimientoFisico();
    }

    void DetectarEnemigoEnFrente()
    {
        Vector2 direccion = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 origen = (Vector2)transform.position + direccion * offsetParry;

        RaycastHit2D hit = Physics2D.Raycast(origen, direccion, distanciaParry, capaEnemigos);

        if (hit.collider != null)
        {
            enemyEnFrente = hit.collider.GetComponentInParent<Enemy_Controller>();
        }
        else
        {
            enemyEnFrente = null;
        }
    }

    public void CambiarEstado(EstadoJugador nuevoEstado)
    {
        estadoActual = nuevoEstado;
    }

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Vector2 direccion = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            Vector2 origen = (Vector2)transform.position + direccion * offsetParry;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(origen, origen + direccion * distanciaParry);
        }
    }
}
