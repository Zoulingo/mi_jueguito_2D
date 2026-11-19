/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Dano por contacto: el enemigo lastima al jugador al tocarlo.
Uso: Va en el orco y en el imp.
*/
using UnityEngine;

/// <summary>
/// Hace dano al jugador cuando el enemigo lo toca.
/// </summary>
public class DanoContacto : MonoBehaviour
{
    public int dano = 1;   // vida que quita cada golpe
    public bool terminaElJuego = true;   // true = un toque y se acaba la partida
    public float tiempoEntreGolpes = 1f;   // segundos de espera entre un golpe y otro
    private float proximoGolpe;   // momento en que puede volver a golpear

    // Mientras el enemigo toca al jugador le hace dano (una vez cada tiempoEntreGolpes segundos)
    void OnCollisionStay2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        if (Time.time < proximoGolpe) return;

        Vida vida = col.gameObject.GetComponent<Vida>();
        if (vida != null)
        {
            vida.RecibirDano(terminaElJuego ? 9999 : dano);
            proximoGolpe = Time.time + tiempoEntreGolpes;
        }
    }
}
