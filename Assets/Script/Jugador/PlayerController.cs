/*
Autor:psotoc
Fecha:25/10/2024
Licencia:Open estudiantil
Funcionalidad: mueve el objeto de iz. a dr. y con la espaciadora salta.
Modificación: Clase que se asigna al objeto de juego
*/
using UnityEngine;

/// <summary>
/// Coordina al jugador: lee el teclado y delega en Movement, Jump, Flip y JumpPowerAdjuster.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
[RequireComponent(typeof(Flip))]
[RequireComponent(typeof(JumpPowerAdjuster))]

public class PlayerController : MonoBehaviour
{
    // Referencias a los componentes (objetos de otras clases) que coordina PlayerController
    private Rigidbody2D rb;
    private Movement movement;
    private Jump jump;
    private Flip flip;
    private JumpPowerAdjuster jumpPowerAdjuster;

    // Obtiene los componentes del mismo objeto y avisa en la consola si falta alguno
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		if (rb == null) Debug.LogError("Rigidbody2D no asignado");
		rb.freezeRotation = true;

		movement = GetComponent<Movement>();
		if (movement == null) Debug.LogError("Movement no asignado");

		jump = GetComponent<Jump>();
		if (jump == null) Debug.LogError("Jump no asignado");

		flip = GetComponent<Flip>();
		if (flip == null) Debug.LogError("Flip no asignado");

		jumpPowerAdjuster = GetComponent<JumpPowerAdjuster>();
		if (jumpPowerAdjuster == null) Debug.LogError("JumpPowerAdjuster no asignado");

    }

    // Cada cuadro lee el teclado y delega el trabajo en cada componente
    void Update()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");
        movement.Move(rb, moveInput);

        // Verificar la dirección en la que mira el personaje
        flip.CheckFlip(moveInput);

        // Intentar saltar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump.TryJump(rb);
        }

        // Ajustar la potencia de salto
        jumpPowerAdjuster.AdjustJumpForce(jump);
    }
}
