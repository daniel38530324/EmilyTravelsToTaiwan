using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [field:SerializeField] public bool isBall{ get; private set; }
    [SerializeField] private float speed = 40;
    
    private Transform ballImage;
    private int randomDirection;

    private void Start() {
        ballImage = transform.GetChild(0);

        randomDirection = Random.Range(-2, 2);
        if(randomDirection == 0){
            randomDirection = 1;
        }
        else if(randomDirection == -2){
            randomDirection = -1;
        }

        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        ballImage.Rotate(0, 0, randomDirection*speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Level7_GameManager.Instance.CheckBall(this);
            Destroy(gameObject);
        }
    }
}
