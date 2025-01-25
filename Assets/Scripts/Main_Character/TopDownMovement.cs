using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    private float sprintingSpeed;
    public float rotationSpeed = 10f;
    private Vector3 moveDirection;
    public InputAction movement, sprint;
    public bool isCarryingItem = false;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezePositionY;
        sprintingSpeed = speed * 1.25f;
    }

    private void OnEnable()
    {
        movement.Enable();
        sprint.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        sprint.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float actualSpeed = sprint.IsPressed() ? sprintingSpeed : speed;

        Vector2 input = movement.ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0, input.y).normalized;

        rb.MovePosition(rb.position + moveDirection * actualSpeed * Time.fixedDeltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public bool IsCarryingItem()
    {
        return isCarryingItem;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Client") && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Interacción con: " + other.name);
            InteractWithStation(other);
        }
    }

    private void InteractWithStation(Collider station)
    {
        Debug.Log("Ejecutando lógica de interacción para: " + station.name);
        // Aquí puedes agregar la lógica para recoger un objeto
        isCarryingItem = true; // Ejemplo de cómo podrías cambiar el estado
    }
}
