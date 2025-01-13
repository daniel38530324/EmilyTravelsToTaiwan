using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] int index;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector3 movement;
    private bool isCorrect;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector3(speed, 0, 0);
    }
    private void FixedUpdate() {
        rb.velocity = movement;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            switch(Level1_GameManager.Instance.Level1_GameState){
                case Level1_GameState.Question1:
                    if(index == 1){
                        isCorrect = true;
                    }
                    else{
                        isCorrect = false;
                    }
                    break;
                case Level1_GameState.Question2:
                    if(index == 3){
                        isCorrect = true;
                    }
                    else{
                        isCorrect = false;
                    }
                    break;
                case Level1_GameState.Question3:
                    if(index == 2){
                        isCorrect = true;
                    }
                    else{
                        isCorrect = false;
                    }
                    break;
                case Level1_GameState.Question4:
                    if(index == 4){
                        isCorrect = true;
                    }
                    else{
                        isCorrect = false;
                    }
                    break;
                case Level1_GameState.Question5:
                    if(index == 2){
                        isCorrect = true;
                    }
                    else{
                        isCorrect = false;
                    }
                    break;
                case Level1_GameState.Question6:
                    if(index == 3){
                        isCorrect = true;
                    }
                    else{
                        isCorrect = false;
                    }
                    break;
                case Level1_GameState.Question7:
                    if(index == 1){
                        isCorrect = true;
                    }
                    else{
                        isCorrect = false;
                    }
                    break;
            }
            Level1_GameManager.Instance.UpdatePower(isCorrect);
            //Level1_GameManager.instance.UpdateLevel1_GameState_Later(2);
            Level1_GameManager.Instance.HandleBatrry();
        }
    }
}
