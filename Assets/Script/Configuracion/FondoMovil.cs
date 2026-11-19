/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Capas de fondo con efecto parallax que ademas se mueven solas (cielo, nubes y castillos).
Uso: Va en cada capa de fondo (Fondo_Cielo, Fondo_Nubes, etc.).
*/
using UnityEngine;

/// <summary>
/// Capa de fondo que se repite sin cortes (parallax) y se desplaza sola.
/// </summary>
public class FondoMovil : MonoBehaviour
{
    public float velocidadPropia = 0.5f;  // cuanto se mueve solo (unidades por segundo)
    public float parallax = 0.3f;         // cuanto se mueve al avanzar el mago (0 = nada, 1 = como el escenario)
    public float desfaseY = 0f;   // ajuste de altura respecto a la camara
    public int ordenCapa = -30;   // orden de dibujo: mas bajo = mas al fondo

    private SpriteRenderer sr;   // dibuja la imagen del fondo
    private Transform cam;   // camara principal
    private float anchoTile;   // ancho de una imagen del fondo

    // Prepara la capa: la imagen se repite (Tiled) 4 veces a lo ancho
    void Start()
    {
        cam = Camera.main.transform;
        sr = GetComponent<SpriteRenderer>();
        anchoTile = sr.sprite.bounds.size.x;
        float alto = sr.sprite.bounds.size.y;

        sr.drawMode = SpriteDrawMode.Tiled;
        sr.size = new Vector2(anchoTile * 4f, alto);
        sr.sortingOrder = ordenCapa;
    }

    // Calcula cuanto se desplazo el fondo (por el parallax y por su velocidad propia)
    // y lo coloca siguiendo a la camara. Mathf.Repeat evita saltos al repetirse la imagen
    void LateUpdate()
    {
        float desplazamiento = Mathf.Repeat(cam.position.x * parallax + Time.time * velocidadPropia, anchoTile);
        transform.position = new Vector3(cam.position.x - desplazamiento, cam.position.y + desfaseY, 0f);
    }
}