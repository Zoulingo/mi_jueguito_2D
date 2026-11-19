/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Efecto de destello rojo cuando un personaje recibe dano.
Uso: Va en el mago y en los enemigos (necesita un SpriteRenderer).
*/
using UnityEngine;

/// <summary>
/// Efecto visual al recibir dano: el personaje se tine de rojo un instante.
/// </summary>
public class EfectoDano : MonoBehaviour
{
    public Sprite spriteDano;        // opcional: cuadro de "herido"
    public float duracion = 0.25f;   // segundos que dura el efecto
    public Color colorDano = new Color(1f, 0.4f, 0.4f);

    private SpriteRenderer sr;   // dibujo del personaje
    private float finEfecto;   // momento en que termina el efecto

    // Obtiene el SpriteRenderer del mismo objeto
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Lo llama Vida con SendMessage cada vez que el personaje recibe dano
    void AlRecibirDano()
    {
        finEfecto = Time.time + duracion;
    }

    // Mientras dura el efecto pinta el sprite de rojo; despues vuelve al color normal
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