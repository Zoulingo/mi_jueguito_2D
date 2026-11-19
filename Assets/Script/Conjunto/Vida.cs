using System.Collections;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int puntosAlMorir = 0;
    public float retrasoMuerte = 0.5f;   // solo para el jugador: tiempo que se ve el corazon vacio
    private int vidaActual;
    private bool muriendo;

    public int VidaActual { get { return vidaActual; } }

    void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDano(int cantidad)
    {
        if (muriendo) return;

        vidaActual = Mathf.Max(0, vidaActual - cantidad);
        SendMessage("AlRecibirDano", SendMessageOptions.DontRequireReceiver);

        if (AudioManager.Instance != null)
        {
            // si es un enemigo, suena "generar daño" (tu hechizo le pegó)
            // si es el mago, suena "recibir daño"
            if (puntosAlMorir > 0)
                AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoGenerarDano);
            else
                AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoRecibirDano);
        }

        if (vidaActual <= 0)
        {
            if (puntosAlMorir > 0 && GameManager.Instance != null)
            {
                GameManager.Instance.SumarPuntos(puntosAlMorir);
            }

            // el jugador (puntosAlMorir = 0) se queda un momento para ver el corazon vacio
            if (puntosAlMorir == 0 && retrasoMuerte > 0f)
                StartCoroutine(MorirConRetraso());
            else
                Destroy(gameObject);
        }
    }

    IEnumerator MorirConRetraso()
    {
        muriendo = true;

        // desactiva controles y magia, pero deja la barra de corazones funcionando
        foreach (MonoBehaviour mb in GetComponents<MonoBehaviour>())
        {
            if (mb != this && !(mb is BarraCorazones)) mb.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(retrasoMuerte);
        Destroy(gameObject);
    }
}