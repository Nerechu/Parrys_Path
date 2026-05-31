using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class SaludJugador : MonoBehaviour
{
    [Header("Estadísticas")]
    public int vidaActual = 6;
    public bool estaSaltando = false;
    public bool estaMuriendo = false; 

    [Header("Configuración Visual")]
    public Image[] corazonesUI; 
    public Sprite corazonLleno;
    public Sprite corazonMitad;
    public Sprite corazonVacio;

    [Header("Trampas y Caídas")]
    public float margenDeCaida = 0.15f; // Los segundos de "perdón" antes de caer
    private float tiempoPisandoTrampa = 0f; // Cronómetro interno

    // --- DETECCIÓN DE ENEMIGOS ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            RecibirDano();
        }
    }

    // --- DETECCIÓN DE AGUA Y PRECIPICIOS (Mientras pisa) ---
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua") || collision.CompareTag("Precipicio"))
        {
            if (estaSaltando == false && estaMuriendo == false)
            {
                // El cronómetro avanza mientras esté pisando el agua
                tiempoPisandoTrampa += Time.deltaTime;

                // Si supera el tiempo de perdón, se cae
                if (tiempoPisandoTrampa >= margenDeCaida)
                {
                    StartCoroutine(SecuenciaDeCaida());
                }
            }
        }
    }

    // --- SI LOGRA SALIR DE LA TRAMPA A TIEMPO ---
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua") || collision.CompareTag("Precipicio"))
        {
            // Reseteamos el cronómetro a 0 porque se ha salvado
            tiempoPisandoTrampa = 0f; 
        }
    }

    // --- DETECCIÓN DE LA META ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meta"))
        {
            SceneManager.LoadScene(3); 
        }
    }

    // --- LÓGICA DE RECIBIR DAÑO ---
    public void RecibirDano()
    {
        vidaActual -= 1; 
        ActualizarCorazones(); 

        if (vidaActual <= 0)
        {
            SceneManager.LoadScene(2); 
        }
    }

    void ActualizarCorazones()
    {
        for (int i = 0; i < corazonesUI.Length; i++)
        {
            if (vidaActual >= (i + 1) * 2)
            {
                corazonesUI[i].sprite = corazonLleno;
            }
            else if (vidaActual == (i * 2) + 1)
            {
                corazonesUI[i].sprite = corazonMitad;
            }
            else
            {
                corazonesUI[i].sprite = corazonVacio;
            }
        }
    }

    // --- SECUENCIA DE CAÍDA (CORRUTINA) ---
    System.Collections.IEnumerator SecuenciaDeCaida()
    {
        estaMuriendo = true;

        // 1. Bloqueamos el movimiento
        Player_Controller controller = GetComponent<Player_Controller>();
        if (controller != null && controller.moveModule != null)
        {
            controller.moveModule.movimientoBloqueado = true;
        }

        // 2. Avisamos a la animación (Si ya configuraste el Animator de la fase anterior)
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Caer");
        }

        // 3. Esperamos 1 segundo para ver la caída
        yield return new WaitForSeconds(1f);

        // 4. Game Over
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}