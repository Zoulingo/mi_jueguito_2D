using UnityEngine;

public class SonidoAlAparecer : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoHechizo);
    }
}