using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enemy_Seguimiento
{
    [Header("Movimiento")]
    public float speed = 2f;
    public float distanciaDeteccion = 4f;
    public float distanciaMinima = 0.6f;

    private Rigidbody2D rb;
    private Transform enemyTransform;
    private Transform objetivo;

    public void Initialize(Rigidbody2D rb, Transform transform)
    {
        this.rb = rb;
        this.enemyTransform = transform;
        objetivo = GameObject.FindGameObjectWithTag("Player")?.transform;

        transform.GetComponent<MonoBehaviour>().StartCoroutine(CorutinaSeguimiento());
    }

    IEnumerator CorutinaSeguimiento()
    {
        while (true)
        {
            if (objetivo != null)
            {
                float dist = Vector2.Distance(enemyTransform.position, objetivo.position);

                if (dist < distanciaDeteccion && dist > distanciaMinima)
                {
                    Vector2 dir = (objetivo.position - enemyTransform.position).normalized;
                    rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
