/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Enemigo (orco) que camina de lado a lado sin caerse de la plataforma. Hereda de Enemigo.
Uso: Va en el prefab Enemigo_Orco.
*/
using UnityEngine;

// Orco: camina de un lado a otro sin caerse de la plataforma
public class EnemigoPatrulla : Enemigo
{
    public float velocidad = 2f;   // velocidad al caminar
    public bool evitarCaida = true;   // si es true, gira al llegar al borde de la plataforma
    public float adelante = 0.7f;        // a que distancia del centro revisa si hay suelo
    public float distanciaSuelo = 1f;
    private int direccion = 1;   // 1 = derecha, -1 = izquierda

    // Implementa el metodo abstracto de Enemigo: camina y gira en los bordes y en las paredes
    protected override void Mover()
    {
        if (evitarCaida)
        {
            // lanza un rayo hacia abajo delante del enemigo; si no hay suelo, da la vuelta
            Vector2 origen = (Vector2)transform.position + new Vector2(direccion * adelante, 0);
            RaycastHit2D hit = Physics2D.Raycast(origen, Vector2.down, distanciaSuelo);
            if (hit.collider == null) Girar();
        }

        rb.velocity = new Vector2(direccion * velocidad, rb.velocity.y);
    }

    // Si choca con una pared (contacto lateral) da la vuelta
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Suelo_01")) return;

        foreach (ContactPoint2D c in col.contacts)
        {
            if (Mathf.Abs(c.normal.x) > 0.5f)
            {
                Girar();
                return;
            }
        }
    }

    // Invierte la direccion y voltea el sprite (Voltear viene de la clase Enemigo)
    void Girar()
    {
        direccion *= -1;
        Voltear(direccion);
    }
}
