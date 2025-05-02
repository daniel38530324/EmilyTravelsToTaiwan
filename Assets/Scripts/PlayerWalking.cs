using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    //[SerializeField] private SpriteRenderer spriteRenderer;

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
        if(!Level6_GameManager.Instance.PlayerCanMove){
            rb.velocity = new Vector2(0, 0);
            return;
        }

        rb.velocity = (Vector2)transform.right * previousMovementInput.x * movementSpeed + (Vector2)transform.up * previousMovementInput.y * movementSpeed;
    }

    private void HandleMove(Vector2 movementInput){
        if(!Level6_GameManager.Instance.PlayerCanMove){
            previousMovementInput = new Vector2(0, 0);
            return;
        }

        previousMovementInput = movementInput;
        if(previousMovementInput.x > 0){
            transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(previousMovementInput.x < 0){
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("End")){
            Level6_GameManager.Instance.UpdateLevel6_GameState(Level6_GameState.Success);
        }
    }
}
