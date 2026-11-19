using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f;
    public int dano = 1;
    public float tiempoDeVida = 3f;
    [HideInInspector] public Vector2 direccion = Vector2.right;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Suelo_01"))
        {
            Destroy(gameObject);
            return;
        }

        Vida vida = other.GetComponent<Vida>();
        if (vida != null)
        {
            vida.RecibirDano(dano);
            Destroy(gameObject);
        }
    }
}