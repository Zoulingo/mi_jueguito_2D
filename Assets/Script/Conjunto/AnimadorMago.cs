/*
Autor: Bryan
Fecha: Noviembre 2026
Licencia: Open estudiantil
Funcionalidad: Cambia entre la animacion quieto y caminar segun la velocidad del personaje.
Uso: Va en el mago, el orco y el imp (todos con un Animator con el parametro Velocidad).
*/
using UnityEngine;

/// <summary>
/// Envia la velocidad horizontal al Animator para cambiar entre quieto y caminar.
/// </summary>
public class AnimadorMago : MonoBehaviour
{
    private Animator animator;   // controla las animaciones
    private Rigidbody2D rb;   // cuerpo fisico (de aqui se lee la velocidad)
    private bool tieneParametro;   // true si el Animator tiene el parametro Velocidad

    // Obtiene los componentes y avisa si al Animator le falta el parametro Velocidad
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogWarning(gameObject.name + " no tiene Animator", gameObject);
            enabled = false;
            return;
        }

        foreach (AnimatorControllerParameter p in animator.parameters)
        {
            if (p.name == "Velocidad") tieneParametro = true;
        }

        if (!tieneParametro)
        {
            Debug.LogWarning("A " + gameObject.name + " le falta el parametro Velocidad en su Animator", gameObject);
        }
    }

    // Cada cuadro actualiza el parametro Velocidad (el Animator cambia de Idle a Run segun su valor)
    void Update()
    {
        if (tieneParametro) animator.SetFloat("Velocidad", Mathf.Abs(rb.velocity.x));
    }
}