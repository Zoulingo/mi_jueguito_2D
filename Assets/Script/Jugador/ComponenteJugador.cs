/*
Clase base de las partes que controlan al jugador (Movement, Jump y Flip heredan de ella).
*/
using UnityEngine;

public abstract class ComponenteJugador : MonoBehaviour
{
    private Rigidbody2D cuerpo;

    // Rigidbody2D del jugador, se busca una sola vez
    protected Rigidbody2D Cuerpo
    {
        get
        {
            if (cuerpo == null) cuerpo = GetComponent<Rigidbody2D>();
            return cuerpo;
        }
    }
}
