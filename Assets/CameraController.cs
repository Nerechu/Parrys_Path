using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player")]
    public Transform target;
    public float followSmooth = 0.15f;
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Zoom")]
    public float normalZoom = 5f;
    private Camera cam;

    private bool enSala = false;
    private Vector3 salaPos;
    private float salaZoom;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = normalZoom;
    }

    void LateUpdate()
    {
        if (!enSala)
        {
            Vector3 desired = target.position + offset;
            Vector3 smoothed = Vector3.Lerp(transform.position, desired, followSmooth);

            transform.position = new Vector3(smoothed.x, smoothed.y, offset.z);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, normalZoom, 0.1f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, salaPos, 0.1f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, salaZoom, 0.1f);
        }
    }

    public void EntrarSala(Vector3 pos, float zoom)
    {
        enSala = true;
        salaPos = pos;
        salaZoom = zoom;
    }

    public void SalirSala()
    {
        enSala = false;
    }
}
