/*
Autor:psotoc
Fecha:25/10/2024
Licencia:Open estudiantil
Funcionalidad: mueve el objeto de iz. a dr. y con la espaciadora salta.
Modificación: Clase que hace saltar al personaje
Modificación 2: solo se considera suelo si el contacto viene desde abajo
Modificación 3: hereda de ComponenteJugador
*/
using UnityEngine;

public class Jump : ComponenteJugador
{
    public float jumpForce = 10f; // Fuerza del salto inicial
    private bool isGrounded = true;   // true si esta pisando el suelo

    // Salta solo si esta en el suelo (impulso hacia arriba)
    public void TryJump(Rigidbody2D rb)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    // Mientras toca algo con el tag Suelo_01 revisa que el contacto venga desde abajo (normal.y > 0.5)
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Suelo_01")) return;

        foreach (ContactPoint2D contacto in collision.contacts)
        {
            if (contacto.normal.y > 0.5f && Cuerpo.velocity.y < 0.1f)
            {
                isGrounded = true;
                return;
            }
        }
    }
}
