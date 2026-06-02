using UnityEngine;
using UnityEngine.UI;

public class ControladorVolumen : MonoBehaviour
{
    // Esta función recibe el número del Slider y se lo pasa al volumen general
    public void CambiarVolumen(float valorSlider)
    {
        AudioListener.volume = valorSlider;
    }
}