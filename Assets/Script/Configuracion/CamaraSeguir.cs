using UnityEngine;

public class CamaraSeguir : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 5f;
    public float limiteInferior = -3f;   // la camara nunca baja de esta altura (Y)

    void LateUpdate()
    {
        if (objetivo == null) return;

        float y = Mathf.Max(objetivo.position.y, limiteInferior);
        Vector3 destino = new Vector3(objetivo.position.x, y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
    }
}