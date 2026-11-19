/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Estira la lava (cuerpo y superficie) al ancho y alto indicados.
Uso: Va en Lava_Cuerpo y Lava_Superficie.
*/
using UnityEngine;

/// <summary>
/// Ajusta el tamano de la imagen de lava, que se repite en mosaico.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class LavaAncho : MonoBehaviour
{
    public float ancho = 60f;   // largo de la lava
    public float alto = 1f;   // alto de la lava

    private SpriteRenderer sr;

    // Pone el SpriteRenderer en modo Tiled (la imagen se repite)
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Tiled;
    }

    // Mantiene el tamano configurado
    void LateUpdate()
    {
        sr.size = new Vector2(ancho, alto);
    }
}