using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Level1_GameManager.Instance.CheckHealth();
            Level1_GameManager.Instance.HandlePlayerRespawn();
        }
    }
}
