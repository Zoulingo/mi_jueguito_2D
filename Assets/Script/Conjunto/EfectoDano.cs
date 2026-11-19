using UnityEngine;

public class EfectoDano : MonoBehaviour
{
    public Sprite spriteDano;        // opcional: cuadro de "herido"
    public float duracion = 0.25f;
    public Color colorDano = new Color(1f, 0.4f, 0.4f);

    private SpriteRenderer sr;
    private float finEfecto;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void AlRecibirDano()
    {
        finEfecto = Time.time + duracion;
    }

    void LateUpdate()
    {
        if (Time.time < finEfecto)
        {
            sr.color = colorDano;
            if (spriteDano != null) sr.sprite = spriteDano;
        }
        else
        {
            sr.color = Color.white;
        }
    }
}