using UnityEngine;

public class LavaAnimada : MonoBehaviour
{
    public Sprite[] cuerpo;
    public Sprite[] superficie;
    public float ancho = 120f;
    public float alto = 6f;
    public float cuadrosPorSegundo = 6f;
    public int ordenCapa = 3;

    private SpriteRenderer srCuerpo;
    private SpriteRenderer srSuperficie;

    void Start()
    {
        srCuerpo = CrearCapa("Cuerpo", cuerpo[0], ancho, alto - 1f, -0.5f, ordenCapa);
        srSuperficie = CrearCapa("Superficie", superficie[0], ancho, 1f, alto / 2f - 0.5f, ordenCapa + 1);

        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(ancho, alto - 0.4f);
        col.offset = new Vector2(0f, -0.2f);
    }

    SpriteRenderer CrearCapa(string nombre, Sprite sprite, float w, float h, float y, int orden)
    {
        GameObject go = new GameObject(nombre);
        go.transform.SetParent(transform);
        go.transform.localPosition = new Vector3(0f, y, 0f);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.drawMode = SpriteDrawMode.Tiled;
        sr.size = new Vector2(w, h);
        sr.sortingOrder = orden;
        return sr;
    }

    void Update()
    {
        int i = (int)(Time.time * cuadrosPorSegundo);
        srCuerpo.sprite = cuerpo[i % cuerpo.Length];
        srCuerpo.size = new Vector2(ancho, alto - 1f);
        srSuperficie.sprite = superficie[i % superficie.Length];
        srSuperficie.size = new Vector2(ancho, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vida v = other.GetComponent<Vida>();
        if (v != null) v.RecibirDano(999);   // la lava mata de un toque
    }
}