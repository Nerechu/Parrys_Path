using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public enum EstadoEnemigo
    {
        Idle,
        Persiguiendo,
        Atacando,
        Parried,
        Muerto
    }

    [Header("Estado actual")]
    public EstadoEnemigo estadoActual = EstadoEnemigo.Idle;

    [Header("Módulos del enemigo")]
    public Enemy_Health healthModule;
    public Enemy_Seguimiento seguimientoModule;
    public Enemy_Attack attackModule;
    public Enemy_ParryDetector parryModule;   // ⭐ NUEVO

    private Animator animator;
    private Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Inicializar módulos
        if (healthModule != null)
            healthModule.Initialize(this, animator);

        if (seguimientoModule != null)
            seguimientoModule.Initialize(this, animator, rb, transform);

        if (attackModule != null)
            attackModule.Initialize(this, animator, transform);

        if (parryModule != null)
            parryModule.Initialize(this, animator, rb);   // ⭐ NUEVO
    }

    void Update()
    {
        if (estadoActual == EstadoEnemigo.Muerto)
            return;

        if (estadoActual == EstadoEnemigo.Parried)
            return; // ⭐ No moverse ni atacar mientras está stuneado

        if (seguimientoModule != null)
            seguimientoModule.ActualizarLogica();

        if (attackModule != null)
            attackModule.ActualizarAtaque();
    }

    void FixedUpdate()
    {
        if (estadoActual == EstadoEnemigo.Muerto)
            return;

        if (estadoActual == EstadoEnemigo.Parried)
            return; // ⭐ No moverse mientras está stuneado

        if (seguimientoModule != null)
            seguimientoModule.ActualizarMovimientoFisico();
    }

    public void CambiarEstado(EstadoEnemigo nuevoEstado)
    {
        estadoActual = nuevoEstado;
    }
}
