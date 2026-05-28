using UnityEngine;

[System.Serializable]
public class Enemy_Seguimiento : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform enemyTransform;
    private Transform objetivo;

    [Header("Movimiento")]
    public float speed = 2f;
    public float distanciaDeteccion = 4f;
    public float distanciaMinima = 0.6f;

    // 👉 Inicialización universal (sin controllers)
    public void Initialize(Rigidbody2D rb, Transform transform)
    {
        this.rb = rb;
        this.enemyTransform = transform;

        objetivo = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void ActualizarLogica()
    {
        // Ya no hace nada más que comprobar distancias si quieres usarlo luego
        if (objetivo == null || enemyTransform == null)
            return;
    }

    public void ActualizarMovimientoFisico()
    {
        if (objetivo == null || enemyTransform == null || rb == null)
            return;

        float dist = Vector2.Distance(enemyTransform.position, objetivo.position);

        // Fuera de rango → no se mueve
        if (dist > distanciaDeteccion)
            return;

        // Demasiado cerca → no se pega más
        if (dist < distanciaMinima)
            return;

        Vector2 dir = (objetivo.position - enemyTransform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
