using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
      // Esta función recibe el tick del Toggle y activa o desactiva la pantalla completa
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}