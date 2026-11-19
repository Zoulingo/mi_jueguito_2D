/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Reproduce el sonido de hechizo cuando aparece la bola de fuego.
Uso: Va en el prefab BolaFuego.
*/
using UnityEngine;

/// <summary>
/// Reproduce el sonido del hechizo cuando el objeto aparece.
/// </summary>
public class SonidoAlAparecer : MonoBehaviour
{
    // Start se ejecuta una vez, cuando el objeto aparece en la escena
    void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoHechizo);
    }
}