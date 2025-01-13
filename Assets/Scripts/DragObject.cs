using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float movementTime = 1000f;
    [SerializeField] private string commodityName;

    private Vector3 currentPos, previousPos, originPos;

    private void OnMouseDown() {
        originPos = transform.position;
        //GetComponent<BoxCollider2D>().enabled = false;
        Vector3 mouPos = Mouse.current.position.ReadValue();
        mouPos.z = 10;
        previousPos = Camera.main.ScreenToWorldPoint(mouPos);
    }

    private void OnMouseDrag() {
        Vector3 mouPos = Mouse.current.position.ReadValue();
        mouPos.z = 10;
        currentPos = Camera.main.ScreenToWorldPoint(mouPos);
        Vector3 newPos = transform.position + (currentPos - previousPos);
        //transform.position = newPos;
        transform.position = Vector3.Lerp(transform.position, newPos, movementTime * Time.deltaTime);
        previousPos = currentPos;
    }

    private void OnMouseUp() {
        //GetComponent<BoxCollider2D>().enabled = true;
        transform.position = originPos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Bucket"){
            Level2_GameManager.Instance.UpdateCommodity(commodityName);
            Destroy(gameObject);
        }
    }
}


