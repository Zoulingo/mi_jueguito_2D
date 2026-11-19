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
    private bool isGrounded = true;

    public void TryJump(Rigidbody2D rb)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

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
