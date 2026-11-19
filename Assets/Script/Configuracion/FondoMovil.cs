using UnityEngine;

public class FondoMovil : MonoBehaviour
{
    public float velocidadPropia = 0.5f;  // cuanto se mueve solo (unidades por segundo)
    public float parallax = 0.3f;         // cuanto se mueve al avanzar el mago (0 = nada, 1 = como el escenario)
    public float desfaseY = 0f;
    public int ordenCapa = -30;

    private SpriteRenderer sr;
    private Transform cam;
    private float anchoTile;

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

    void LateUpdate()
    {
        float desplazamiento = Mathf.Repeat(cam.position.x * parallax + Time.time * velocidadPropia, anchoTile);
        transform.position = new Vector3(cam.position.x - desplazamiento, cam.position.y + desfaseY, 0f);
    }
}