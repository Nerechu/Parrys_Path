using UnityEngine;

public class SalaCamara : MonoBehaviour
{
    public Transform centroSala;
    public float zoomSala = 8f;

    private CameraController cam;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.EntrarSala(centroSala.position, zoomSala);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.SalirSala();
        }
    }
}
