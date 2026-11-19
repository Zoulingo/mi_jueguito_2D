/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Dispara una bola de fuego hacia donde esta el mouse al hacer clic izquierdo.
Uso: Va en el mago. Se asigna el prefab BolaFuego.
*/
using UnityEngine;

/// <summary>
/// Lanza una bola de fuego hacia el mouse al hacer clic izquierdo.
/// </summary>
public class LanzadorMagia : MonoBehaviour
{
    public GameObject proyectilPrefab;   // prefab de la bola de fuego
    public float cooldown = 0.5f;   // segundos entre disparos
    private float proximoDisparo;   // momento en que puede volver a disparar

    // Clic izquierdo = dispara (respetando el tiempo de espera)
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= proximoDisparo)
        {
            proximoDisparo = Time.time + cooldown;
            Disparar();
        }
    }

    // Calcula la direccion hacia el mouse, crea el proyectil rotado hacia alla y reproduce el sonido
    void Disparar()
    {
        Vector3 mouseMundo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = ((Vector2)mouseMundo - (Vector2)transform.position).normalized;

        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        GameObject obj = Instantiate(proyectilPrefab, transform.position, Quaternion.Euler(0, 0, angulo));
        obj.GetComponent<Proyectil>().direccion = direccion;

        if (AudioManager.Instance != null)
            AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoHechizo);
    }
}