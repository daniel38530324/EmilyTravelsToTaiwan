using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    [Header("Setting")]
    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;
    private Vector2 previousMovementInput;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        inputReader.MoveEvent += HandleMove;
    }

    private void OnDestroy() {
        inputReader.MoveEvent -= HandleMove;
    }

    private void FixedUpdate() {
        rb.velocity = (Vector2)transform.right * previousMovementInput.x * movementSpeed + (Vector2)transform.up * previousMovementInput.y * movementSpeed;
    }

    private void HandleMove(Vector2 movementInput){
        previousMovementInput = movementInput;
    }
}
