/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Reproduce los efectos de sonido (hechizo, dano, pasos, puntaje y fin del juego).
Uso: Va en el objeto AudioManager junto a un AudioSource. Los demas scripts lo llaman con AudioManager.Instance.
*/
using UnityEngine;

/// <summary>
/// Centraliza los efectos de sonido del juego (singleton).
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;   // Singleton: una sola instancia accesible desde cualquier script

    // Clips de audio que se arrastran desde el Inspector
    public AudioClip sonidoRecibirDano;
    public AudioClip sonidoGenerarDano;
    public AudioClip sonidoPuntaje;
    public AudioClip sonidoHechizo;
    public AudioClip sonidoMuerteJugador;
    public AudioClip sonidoGameOver;
    public AudioClip[] sonidosPaso;   // varios pasos distintos, se elige uno al azar

    [Range(0f, 1f)] public float volumenEfectos = 0.5f;   // volumen general de los efectos

    private AudioSource fuente;   // reproductor de sonido del mismo objeto

    // Guarda esta instancia y obtiene el AudioSource
    void Awake()
    {
        Instance = this;
        fuente = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Reproduce un efecto una vez (si el clip existe).
    /// </summary>
    public void Reproducir(AudioClip clip)
    {
        if (clip != null) fuente.PlayOneShot(clip, volumenEfectos);
    }

    /// <summary>
    /// Reproduce un sonido de paso elegido al azar.
    /// </summary>
    public void ReproducirPaso()
    {
        if (sonidosPaso == null || sonidosPaso.Length == 0) return;
        AudioClip clip = sonidosPaso[Random.Range(0, sonidosPaso.Length)];
        fuente.PlayOneShot(clip, volumenEfectos);
    }
}