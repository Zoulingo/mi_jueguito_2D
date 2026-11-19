using UnityEngine;

public class DanoContacto : MonoBehaviour
{
    public int dano = 1;
    public bool terminaElJuego = true;   // true = un toque y se acaba la partida
    public float tiempoEntreGolpes = 1f;
    private float proximoGolpe;

    void OnCollisionStay2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        if (Time.time < proximoGolpe) return;

        Vida vida = col.gameObject.GetComponent<Vida>();
        if (vida != null)
        {
            vida.RecibirDano(terminaElJuego ? 9999 : dano);
            proximoGolpe = Time.time + tiempoEntreGolpes;
        }
    }
}
