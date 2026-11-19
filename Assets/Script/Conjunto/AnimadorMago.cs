using UnityEngine;

public class AnimadorMago : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool tieneParametro;

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

    void Update()
    {
        if (tieneParametro) animator.SetFloat("Velocidad", Mathf.Abs(rb.velocity.x));
    }
}