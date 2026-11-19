using UnityEngine;

// Clase base de todos los enemigos. Cada enemigo define su propia rutina en Mover().
public abstract class Enemigo : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        Mover();
    }

    // Rutina de movimiento automatico (en loop) de cada tipo de enemigo
    protected abstract void Mover();

    protected void Voltear(float dir)
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * Mathf.Sign(dir);
        transform.localScale = escala;
    }
}
