using UnityEngine;

// Orco: camina de un lado a otro sin caerse de la plataforma
public class EnemigoPatrulla : Enemigo
{
    public float velocidad = 2f;
    public bool evitarCaida = true;
    public float adelante = 0.7f;        // a que distancia del centro revisa si hay suelo
    public float distanciaSuelo = 1f;
    private int direccion = 1;

    protected override void Mover()
    {
        if (evitarCaida)
        {
            Vector2 origen = (Vector2)transform.position + new Vector2(direccion * adelante, 0);
            RaycastHit2D hit = Physics2D.Raycast(origen, Vector2.down, distanciaSuelo);
            if (hit.collider == null) Girar();
        }

        rb.velocity = new Vector2(direccion * velocidad, rb.velocity.y);
    }

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

    void Girar()
    {
        direccion *= -1;
        Voltear(direccion);
    }
}
