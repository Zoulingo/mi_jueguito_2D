/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Enemigo (imp) que patrulla dando saltos. Hereda de Enemigo.
Uso: Va en el prefab Enemigo_Imp.
*/
using UnityEngine;

// Imp: patrulla de un lado a otro dando saltitos cada cierto tiempo (no persigue al jugador)
public class EnemigoSaltarin : Enemigo
{
    [Header("Patrulla")]
    public float velocidad = 2.5f;
    public float rango = 4f;              // cuanto se aleja a cada lado de su punto inicial
    public float adelante = 0.7f;         // a que distancia del centro revisa si hay suelo
    public float distanciaSuelo = 2.5f;

    [Header("Salto")]
    public float fuerzaSalto = 5f;
    public float intervaloSalto = 1.5f;

    private float xInicial;
    private int dir = 1;
    private float proximoSalto;

    // Redefine el Awake del padre: primero ejecuta el del padre (base.Awake) y luego guarda su punto inicial
    protected override void Awake()
    {
        base.Awake();
        xInicial = transform.position.x;
        proximoSalto = Time.time + intervaloSalto;
    }

    // Implementa el metodo abstracto de Enemigo: patrulla y salta cada cierto tiempo
    protected override void Mover()
    {
        // da la vuelta al llegar al limite de su recorrido
        if (transform.position.x >= xInicial + rango) dir = -1;
        else if (transform.position.x <= xInicial - rango) dir = 1;

        // y tambien si se acaba la plataforma (para no caerse)
        Vector2 origen = (Vector2)transform.position + new Vector2(dir * adelante, 0);
        if (Physics2D.Raycast(origen, Vector2.down, distanciaSuelo).collider == null) dir = -dir;

        // salta solo si esta en el suelo (casi sin velocidad vertical) y ya paso el intervalo
        float vy = rb.velocity.y;
        if (Mathf.Abs(vy) < 0.05f && Time.time >= proximoSalto)
        {
            vy = fuerzaSalto;
            proximoSalto = Time.time + intervaloSalto;
        }

        rb.velocity = new Vector2(dir * velocidad, vy);
        Voltear(dir);
    }

    // Da la vuelta al chocar con una pared
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Suelo_01")) return;

        foreach (ContactPoint2D c in col.contacts)
        {
            if (Mathf.Abs(c.normal.x) > 0.5f)
            {
                dir = -dir;
                return;
            }
        }
    }
}
