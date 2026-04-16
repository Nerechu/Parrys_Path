using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy_Seguimiento
{
    private Enemy_Controller controller;
    private Animator animator;
    private Rigidbody2D rb;
    private Transform enemyTransform;

    [Header("Movimiento")]
    public float speed = 2f;
    public float distanciaDeteccion = 4f;
    public float distanciaMinima = 0.6f; // ⭐ Nueva distancia mínima

    [Header("Objetivo")]
    public Transform objetivo;

    public void Initialize(Enemy_Controller controller, Animator animator, Rigidbody2D rb, Transform transform)
    {
        this.controller = controller;
        this.animator = animator;
        this.rb = rb;
        this.enemyTransform = transform;
    }

    public void ActualizarLogica()
    {
        if (objetivo == null)
            return;

        float dist = Vector2.Distance(enemyTransform.position, objetivo.position);

        if (dist < distanciaDeteccion)
            controller.CambiarEstado(Enemy_Controller.EstadoEnemigo.Persiguiendo);
        else
            controller.CambiarEstado(Enemy_Controller.EstadoEnemigo.Idle);
    }

    public void ActualizarMovimientoFisico()
    {
        if (controller.estadoActual != Enemy_Controller.EstadoEnemigo.Persiguiendo)
            return;

        if (objetivo == null)
            return;

        float dist = Vector2.Distance(enemyTransform.position, objetivo.position);

        // ⭐ Evitar que se meta dentro del player
        if (dist < distanciaMinima)
            return;

        Vector2 dir = (objetivo.position - enemyTransform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
