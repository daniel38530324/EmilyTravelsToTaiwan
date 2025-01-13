using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject bulletEffect, shotEffect;

    private GameObject currentEffect;
    private string currentSound;

    private void Start() {
        inputReader.ShotEvent += Shot;
    }

    private void OnDestroy() {
        inputReader.ShotEvent -= Shot;
    }

    private void Shot(InputAction.CallbackContext context){
        if(Level3_GameManager.Instance.Level3_GameState == Level3_GameState.Game && Level3_GameManager.Instance.CanShot){
            Vector3 mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 100);
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, -1);
            if (hit.collider)
            {
                if(hit.transform.tag == "Balloon"){
                    if(hit.transform.GetComponent<Balloon>().shotCount < 1){
                        hit.transform.GetComponent<Balloon>().shotCount++;
                        currentEffect = shotEffect;
                        currentSound = "Balloon";
                        Debug.Log(hit.transform.name);
                    }
                    else{
                        Debug.Log(hit.transform.name);
                        currentEffect = bulletEffect;
                        currentSound = "Shot";
                    }
                }
            }else{
                currentEffect = bulletEffect;
                currentSound = "Shot";
            }

            AudioManager.Instance.PlaySound(currentSound);
            Instantiate(currentEffect, mousePosition, Quaternion.identity);
        }
    }
}
