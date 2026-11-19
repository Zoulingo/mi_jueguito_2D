/*
Autor:psotoc
Fecha:25/10/2024
Licencia:Open estudiantil
Funcionalidad: mueve el objeto de iz. a dr. y con la espaciadora salta.
Modificación: Clase que realiza el movimiento (con sonido de pasos y correr con Shift)
Modificación 2: hereda de ComponenteJugador
*/
using UnityEngine;

public class Movement : ComponenteJugador
{
    public float moveSpeed = 5f;          // Velocidad de movimiento horizontal
    public float multiplicadorCorrer = 1.6f; // Se multiplica la velocidad al mantener Shift
    public float intervaloPasos = 0.35f;  // Segundos entre cada paso

    private float proximoPaso;   // momento del proximo sonido de paso

    // Mueve al personaje en horizontal (con Shift corre) y reproduce los pasos
    public void Move(Rigidbody2D rb, float moveInput)
    {
        bool corriendo = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float velocidad = corriendo ? moveSpeed * multiplicadorCorrer : moveSpeed;

        rb.velocity = new Vector2(moveInput * velocidad, rb.velocity.y);

        // Sonido de pasos: solo si se mueve y está en el suelo (más rápido al correr)
        float intervalo = corriendo ? intervaloPasos * 0.7f : intervaloPasos;
        if (Mathf.Abs(moveInput) > 0.1f && EstaEnSuelo(rb) && Time.time >= proximoPaso)
        {
            proximoPaso = Time.time + intervalo;
            if (AudioManager.Instance != null)
                AudioManager.Instance.ReproducirPaso();
        }
    }

    // Devuelve true si hay suelo justo debajo de los pies del personaje
    bool EstaEnSuelo(Rigidbody2D rb)
    {
        Collider2D col = rb.GetComponent<Collider2D>();
        if (col == null) return false;

        // Rayo desde los pies hacia abajo, ignorando al propio jugador
        Vector2 origen = new Vector2(col.bounds.center.x, col.bounds.min.y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(origen, Vector2.down, 0.4f);
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider != null && h.collider != col && !h.collider.isTrigger)
                return true;
        }
        return false;
    }
}
