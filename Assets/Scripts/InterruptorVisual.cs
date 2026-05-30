using UnityEngine;
using UnityEngine.UI;

public class InterruptorVisual : MonoBehaviour
{
    public Image imagenInterruptor;
    public Sprite spriteApagado;
    public Sprite spriteEncendido;

    public void ActualizarVisual(bool estaEncendido)
    {
        if (estaEncendido)
        {
            imagenInterruptor.sprite = spriteEncendido;
        }
        else
        {
            imagenInterruptor.sprite = spriteApagado;
        }
    }
}