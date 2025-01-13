using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactive : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite originSprite; 
    [SerializeField] private Sprite glowSprite;
    
    [Header("Setting")]
    [SerializeField] private float scale = 1.2f;

    public void SetScale(bool isSet){
        if(isSet){
            transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
        }
        else{
            transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void SetSprite(bool isSet){
        if(isSet){
            transform.GetChild(0).GetComponent<Image>().sprite = glowSprite;
        }
        else{
            transform.GetChild(0).GetComponent<Image>().sprite = originSprite;
        }
    }
}
