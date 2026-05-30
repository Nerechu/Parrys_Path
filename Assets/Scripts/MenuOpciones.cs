using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    // Esta función recibe el valor decimal del Slider y ajusta el volumen general del juego
    public void CambiarVolumen(float volumen)
    {
        AudioListener.volume = volumen;
    }

    // Esta función recibe el tick del Toggle y activa o desactiva la pantalla completa
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}