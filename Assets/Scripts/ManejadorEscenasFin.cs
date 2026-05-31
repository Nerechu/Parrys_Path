using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorEscenasFin : MonoBehaviour
{
    // Función para el botón de Reintentar
    public void ReintentarJuego()
    {
        // Muy importante: si el jugador murió pausado o el agua congeló el tiempo,
        // nos aseguramos de volver a activar el tiempo normal del juego (1f).
        Time.timeScale = 1f; 
        
        // Carga la escena número 1 (tu nivel del juego)
        SceneManager.LoadScene(1); 
    }

    // Función para el botón de Volver al Menú
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        
        // Carga la escena número 0 (el Menú Principal)
        SceneManager.LoadScene(0); 
    }
}