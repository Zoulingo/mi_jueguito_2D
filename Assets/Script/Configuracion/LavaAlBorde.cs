using UnityEngine;

public class LavaAlBorde : MonoBehaviour
{
    public float altoVisible = 1.5f;   // dónde queda la superficie sobre el borde inferior de la cámara
    public Transform cuerpo;           // arrastra aquí Lava_Cuerpo

    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float bordeInferior = cam.transform.position.y - cam.orthographicSize;
        float x = cam.transform.position.x;

        transform.position = new Vector3(x, bordeInferior + altoVisible, transform.position.z);

        if (cuerpo != null)
        {
            LavaAncho datos = cuerpo.GetComponent<LavaAncho>();
            float altoCuerpo = datos != null ? datos.alto : 3f;

            // la superficie mide 1; el cuerpo arranca 2 píxeles (0.125) bajo su borde para no dejar línea
            float yCuerpo = transform.position.y - 0.375f - altoCuerpo / 2f;
            cuerpo.position = new Vector3(x, yCuerpo, cuerpo.position.z);
        }
    }
}