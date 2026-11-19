/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Controla la vida: recibir dano, morir, sumar puntos y reproducir sonidos.
Uso: Va en el mago y en los enemigos. Se comunica con GameManager y AudioManager.
*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Maneja la vida de un personaje: recibir dano, morir, dar puntos y sonidos.
/// </summary>
public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;   // vida con la que parte
    public int puntosAlMorir = 0;   // puntos que da al morir (0 = es el jugador)
    public float retrasoMuerte = 0.5f;   // solo para el jugador: tiempo que se ve el corazon vacio
    private int vidaActual;   // vida que le queda
    private bool muriendo;   // true mientras espera para desaparecer

    // Propiedad de solo lectura: otros scripts pueden leer la vida pero no cambiarla
    public int VidaActual { get { return vidaActual; } }

    // Al crearse, la vida actual es la maxima
    void Awake()
    {
        vidaActual = vidaMaxima;
    }

    /// <summary>
    /// Resta vida, avisa al efecto de dano, reproduce el sonido y destruye al personaje si llega a 0.
    /// </summary>
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

    // Corrutina: espera un momento antes de destruir al jugador para que se vea el corazon vacio
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