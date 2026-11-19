/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Proyectil del mago: se mueve en linea recta y le quita vida a los enemigos.
Uso: Va en el prefab BolaFuego (collider con Is Trigger).
*/
using UnityEngine;

/// <summary>
/// Bola de fuego: avanza en linea recta y danea al enemigo que toca.
/// </summary>
public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f;   // unidades por segundo
    public int dano = 1;   // vida que quita al enemigo
    public float tiempoDeVida = 3f;   // segundos antes de desaparecer sola
    [HideInInspector] public Vector2 direccion = Vector2.right;

    // Programa la destruccion del proyectil despues de tiempoDeVida
    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    // Avanza en la direccion en que fue lanzado
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    // Se ejecuta al tocar otro collider: ignora al jugador, se destruye con el suelo y danea a quien tenga Vida
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Suelo_01"))
        {
            Destroy(gameObject);
            return;
        }

        Vida vida = other.GetComponent<Vida>();
        if (vida != null)
        {
            vida.RecibirDano(dano);
            Destroy(gameObject);
        }
    }
}