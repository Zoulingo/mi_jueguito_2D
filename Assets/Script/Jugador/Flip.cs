/*
Autor:psotoc
Fecha:25/10/2024
Licencia:Open estudiantil
Funcionalidad: mueve el objeto de iz. a dr. y con la espaciadora salta.
Modificación: Clase que rota personaje
Modificación 2: hereda de ComponenteJugador
*/
using UnityEngine;

public class Flip : ComponenteJugador
{
    private bool facingRight = true;   // true si el personaje mira a la derecha

    // Si el personaje se mueve hacia el lado contrario al que mira, lo voltea
    public void CheckFlip(float moveInput)
    {
        if (moveInput > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (moveInput < 0 && facingRight)
        {
            FlipSprite();
        }
    }

    // Invierte la escala en X (efecto espejo del sprite)
    private void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
