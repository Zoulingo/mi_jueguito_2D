/*
Autor:psotoc
Fecha:25/10/2024
Licencia:Open estudiantil
Funcionalidad: mueve el objeto de iz. a dr. y con la espaciadora salta.
Modificación: Clase que se asigna al objeto de juego
*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
[RequireComponent(typeof(Flip))]
[RequireComponent(typeof(JumpPowerAdjuster))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Movement movement;
    private Jump jump;
    private Flip flip;
    private JumpPowerAdjuster jumpPowerAdjuster;

    void Start()
    {
		/*
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;  // Evita que el objeto rote
		movement = GetComponent<Movement>();
		jump = GetComponent<Jump>();
		flip = GetComponent<Flip>();
		jumpPowerAdjuster = GetComponent<JumpPowerAdjuster>();
		*/
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
