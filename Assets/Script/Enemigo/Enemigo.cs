/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Clase base abstracta de los enemigos (herencia). Define lo comun y obliga a cada hijo a implementar Mover().
Uso: No se pone en un objeto; la usan EnemigoPatrulla y EnemigoSaltarin.
*/
using UnityEngine;

// Clase base de todos los enemigos. Cada enemigo define su propia rutina en Mover().
public abstract class Enemigo : MonoBehaviour
{
    protected Rigidbody2D rb;   // protected: las clases hijas tambien pueden usarlo

    // virtual: las clases hijas pueden redefinirlo (override) llamando a base.Awake()
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Se ejecuta en cada paso de fisica y llama a Mover() (polimorfismo: cada hijo se mueve distinto)
    void FixedUpdate()
    {
        Mover();
    }

    // Rutina de movimiento automatico (en loop) de cada tipo de enemigo
    protected abstract void Mover();

    // Hace que el enemigo mire hacia donde camina (invierte la escala en X)
    protected void Voltear(float dir)
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * Mathf.Sign(dir);
        transform.localScale = escala;
    }
}
