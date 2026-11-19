using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LavaAncho : MonoBehaviour
{
    public float ancho = 60f;
    public float alto = 1f;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Tiled;
    }

    void LateUpdate()
    {
        sr.size = new Vector2(ancho, alto);
    }
}