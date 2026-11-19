/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Muestra la vida del personaje como corazones sobre su cabeza.
Uso: Va en el mago y en los enemigos (necesita el componente Vida).
*/
using UnityEngine;

/// <summary>
/// Dibuja los corazones de vida sobre la cabeza del personaje.
/// </summary>
public class BarraCorazones : MonoBehaviour
{
    // Dibujos de corazon que se arrastran desde el Inspector
    public Sprite corazonLleno;
    public Sprite corazonMedio;
    public Sprite corazonVacio;
    public int corazones = 3;   // cantidad de corazones que se muestran
    public int puntosPorCorazon = 2;   // 2 = medio corazon por golpe, 1 = un corazon por golpe
    public float alturaY = 0.7f;       // que tan arriba de la cabeza
    public float separacion = 0.42f;   // distancia entre corazones
    public float escala = 0.45f;   // tamano de cada corazon

    private Vida vida;   // de aqui se lee la vida actual
    private Transform contenedor;   // objeto que agrupa los corazones
    private SpriteRenderer[] renderers;   // un SpriteRenderer por corazon

    // Crea por codigo el contenedor y los corazones como hijos del personaje (asi lo siguen)
    void Start()
    {
        vida = GetComponent<Vida>();

        contenedor = new GameObject("BarraCorazones").transform;
        contenedor.SetParent(transform);
        contenedor.localPosition = new Vector3(0, alturaY, 0);

        renderers = new SpriteRenderer[corazones];
        float inicio = -(corazones - 1) * separacion / 2f;
        for (int i = 0; i < corazones; i++)
        {
            GameObject h = new GameObject("Corazon" + i);
            h.transform.SetParent(contenedor);
            h.transform.localPosition = new Vector3(inicio + i * separacion, 0, 0);
            h.transform.localScale = new Vector3(escala, escala, 1f);
            renderers[i] = h.AddComponent<SpriteRenderer>();
            renderers[i].sortingOrder = 10;
        }
    }

    // Cada cuadro elige el dibujo de cada corazon: lleno, medio o vacio
    void LateUpdate()
    {
        if (vida == null || contenedor == null) return;

        // si el personaje se voltea, la barra se mantiene derecha
        float signo = Mathf.Sign(transform.lossyScale.x);
        contenedor.localScale = new Vector3(signo, 1f, 1f);

        for (int i = 0; i < corazones; i++)
        {
            int resto = vida.VidaActual - i * puntosPorCorazon;
            if (resto >= puntosPorCorazon) renderers[i].sprite = corazonLleno;
            else if (resto > 0) renderers[i].sprite = corazonMedio;
            else renderers[i].sprite = corazonVacio;
        }
    }
}