using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] float speed;

    private Rigidbody2D rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector3(speed, 0, 0);
        Destroy(gameObject, 10);
    }

    private void Update() {
        transform.Translate(movement * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //rb.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Level1_GameManager.Instance.HandldCarCollision(true);
        }    
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Level1_GameManager.Instance.HandldCarCollision(false);
        }    
    }
}
