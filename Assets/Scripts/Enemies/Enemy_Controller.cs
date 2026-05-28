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
    public Enemy_Attack attackModule;
    public Enemy_ParryDetector parryModule;
    public Enemy_Seguimiento seguimientoModule;

    private Animator animator;
    private Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (healthModule != null)
            healthModule.Initialize(this, animator);

        if (attackModule != null)
            attackModule.Initialize(this, animator, transform);

        if (parryModule != null)
            parryModule.Initialize(this, animator, rb);

        if (seguimientoModule != null)
            seguimientoModule.Initialize(rb, transform);
    }

    void Update()
    {
        if (estadoActual == EstadoEnemigo.Muerto)
            return;

        if (estadoActual == EstadoEnemigo.Parried)
            return;

        if (estadoActual == EstadoEnemigo.Atacando)
            return;

        if (attackModule != null)
            attackModule.ActualizarAtaque();
    }

    void FixedUpdate()
    {
        if (estadoActual == EstadoEnemigo.Muerto)
            return;

        if (estadoActual == EstadoEnemigo.Parried)
            return;

        if (estadoActual == EstadoEnemigo.Atacando)
            return;
    }

    public void CambiarEstado(EstadoEnemigo nuevoEstado)
    {
        estadoActual = nuevoEstado;
    }
}
