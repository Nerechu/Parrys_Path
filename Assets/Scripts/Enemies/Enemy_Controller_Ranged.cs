using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller_Ranged : MonoBehaviour
{
    public enum EstadoEnemigo
    {
        Idle,
        Persiguiendo,
        Atacando,
        Cooldown,
        Muerto
    }

    [Header("Estado actual")]
    public EstadoEnemigo estadoActual = EstadoEnemigo.Idle;

    [Header("Módulos del enemigo")]
    public Enemy_Health healthModule;
    public Enemy_Attack_Ranged rangedAttackModule;

    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (healthModule != null)
            healthModule.Initialize(this, animator);

        if (rangedAttackModule != null)
            rangedAttackModule.Initialize(this, animator, player);
    }

    void Update()
    {
        if (estadoActual == EstadoEnemigo.Muerto)
            return;

        if (rangedAttackModule != null)
            rangedAttackModule.ActualizarAtaque();
    }

    void FixedUpdate()
    {
        if (estadoActual == EstadoEnemigo.Muerto)
            return;

        // El movimiento ahora lo hace Enemy_Seguimiento (MonoBehaviour independiente)
    }

    public void CambiarEstado(EstadoEnemigo nuevoEstado)
    {
        estadoActual = nuevoEstado;
    }
}
