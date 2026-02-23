using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimientoSimple : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float velocidad = 5f;
    public float rotationSpeed = 10f;

    public float gravedad = -9.81f;
    private float velocidadY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input clásico (WASD / Flechas)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical);

        // Rotación del personaje
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Movimiento
        controller.Move(move * velocidad * Time.deltaTime);

        // Gravedad
        if (controller.isGrounded && velocidadY < 0)
            velocidadY = -2f;

        velocidadY += gravedad * Time.deltaTime;
        controller.Move(Vector3.up * velocidadY * Time.deltaTime);

        // Animator (si tienes blend tree)
        if (animator != null)
        {
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
        }
    }
}