using UnityEngine;

[System.Serializable]
public class Enemy_Seguimiento
{
    private Enemy_Controller_Ranged controller;
    private Animator animator;
    private Rigidbody2D rb;
    private Transform enemyTransform;

    [Header("Movimiento")]
    public float speed = 2f;
    public float distanciaDeteccion = 4f;
    public float distanciaMinima = 0.6f;

    [Header("Objetivo")]
    public Transform objetivo;

    public void Initialize(Enemy_Controller_Ranged controller, Animator animator, Rigidbody2D rb, Transform transform)
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
        {
            controller.CambiarEstado(1); // Persiguiendo
            animator.SetBool("Persiguiendo", true);
        }
        else
        {
            controller.CambiarEstado(0); // Idle
            animator.SetBool("Persiguiendo", false);
        }
    }

    public void ActualizarMovimientoFisico()
    {
        if (controller.GetEstadoActual() != 1) // Persiguiendo
            return;

        float dist = Vector2.Distance(enemyTransform.position, objetivo.position);

        if (dist < distanciaMinima)
            return;

        Vector2 dir = (objetivo.position - enemyTransform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
