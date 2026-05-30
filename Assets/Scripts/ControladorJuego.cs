using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour
{
    [Header("Paneles de Interfaz")]
    public GameObject panelPausa;
    public GameObject panelOpciones;

    private bool juegoPausado = false;

    void Update()
    {
        // Detectar si el jugador pulsa la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        panelPausa.SetActive(true); // Enciende el panel visual
        Time.timeScale = 0f;        // ¡Congela el tiempo del juego!
        juegoPausado = true;
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);    // Oculta la pausa
        panelOpciones.SetActive(false); // Oculta opciones por si estaban abiertas
        Time.timeScale = 1f;            // ¡Descongela el tiempo!
        juegoPausado = false;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f; // CRÍTICO: Descongelar el tiempo antes de cambiar de escena
        SceneManager.LoadScene(0); // Carga la escena del menú (índice 0)
    }
}