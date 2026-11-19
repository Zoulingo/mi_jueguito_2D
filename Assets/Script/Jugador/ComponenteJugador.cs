/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Clase base abstracta de las partes del jugador (herencia). Movement, Jump y Flip heredan de ella.
Uso: No se pone en un objeto; solo la heredan otras clases.
*/
/*
Clase base de las partes que controlan al jugador (Movement, Jump y Flip heredan de ella).
*/
using UnityEngine;

public abstract class ComponenteJugador : MonoBehaviour
{
    private Rigidbody2D cuerpo;   // campo privado: solo esta clase lo toca (encapsulamiento)

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
