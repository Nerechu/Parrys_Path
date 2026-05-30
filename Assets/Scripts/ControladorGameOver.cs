using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGameOver : MonoBehaviour
{
    public void ReintentarJuego()
    {
        // El número 1 es la escena de tu Juego. 
        // Por seguridad, descongelamos el tiempo por si el jugador murió estando pausado.
        Time.timeScale = 1f; 
        SceneManager.LoadScene(1); 
    }

    public void VolverAlMenu()
    {
        // El número 0 es la escena del Menú Principal.
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }
}