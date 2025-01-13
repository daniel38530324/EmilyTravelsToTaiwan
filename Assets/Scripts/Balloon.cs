using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int shotCount{get; set;}

    [field: SerializeField] public bool IsChinese {get; private set;} = true;
    [field: SerializeField] public int Id {get; set;} = -1;

    [Header("References")]
    [SerializeField] private Sprite hole;
    [SerializeField] private Transform returnPos;


    [Header("Setting")]
    [SerializeField] private float speed = 5;

    private bool isShot;

    private void Update() {
        if(!IsChinese){
            transform.Translate(0, speed * Time.deltaTime, 0);

            if(transform.position.x < -7.29f){
                transform.position = returnPos.position;
            }
        }
    }

    private void OnMouseDown() 
    {
        if(!Level3_GameManager.Instance.CanShot){return;}

        if(isShot){return;}

        isShot = true;
        GetComponent<SpriteRenderer>().sprite = hole;
        //Instantiate(shotEffect, transform.position, Quaternion.identity);
        Destroy(transform.GetChild(0).gameObject);
        Level3_GameManager.Instance.CheckBalloon(this);
    }
}
