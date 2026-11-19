using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip sonidoRecibirDano;
    public AudioClip sonidoGenerarDano;
    public AudioClip sonidoPuntaje;
    public AudioClip sonidoHechizo;
    public AudioClip sonidoMuerteJugador;
    public AudioClip sonidoGameOver;
    public AudioClip[] sonidosPaso;

    [Range(0f, 1f)] public float volumenEfectos = 0.5f;

    private AudioSource fuente;

    void Awake()
    {
        Instance = this;
        fuente = GetComponent<AudioSource>();
    }

    public void Reproducir(AudioClip clip)
    {
        if (clip != null) fuente.PlayOneShot(clip, volumenEfectos);
    }

    public void ReproducirPaso()
    {
        if (sonidosPaso == null || sonidosPaso.Length == 0) return;
        AudioClip clip = sonidosPaso[Random.Range(0, sonidosPaso.Length)];
        fuente.PlayOneShot(clip, volumenEfectos);
    }
}