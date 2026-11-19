/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: La camara sigue al jugador suavemente y no baja de un limite en Y.
Uso: Va en la Main Camera. Se asigna el jugador en el campo Objetivo.
*/
using UnityEngine;

/// <summary>
/// Hace que la camara siga al jugador de forma suave.
/// </summary>
public class CamaraSeguir : MonoBehaviour
{
    public Transform objetivo;   // a quien sigue (el jugador)
    public float suavizado = 5f;   // mas alto = la camara sigue mas rapido
    public float limiteInferior = -3f;   // la camara nunca baja de esta altura (Y)

    // LateUpdate: se ejecuta despues de que el jugador ya se movio en este cuadro
    void LateUpdate()
    {
        if (objetivo == null) return;

        // la altura nunca baja del limite inferior (asi se ve la lava al caer)
        float y = Mathf.Max(objetivo.position.y, limiteInferior);
        Vector3 destino = new Vector3(objetivo.position.x, y, transform.position.z);
        // Lerp interpola entre la posicion actual y el destino para un movimiento suave
        transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
    }
}