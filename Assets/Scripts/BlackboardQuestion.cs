using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardQuestion : MonoBehaviour
{
    [SerializeField] int id;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Level6_GameManager.Instance.OpenQuestion(id);
            Destroy(gameObject);
        }
    }
}
