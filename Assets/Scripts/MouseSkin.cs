using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseSkin : MonoBehaviour
{
    [SerializeField] Texture2D mouseTexture;
    void Start()
    {
        Vector2 hotSpot;

        if(SceneManager.GetActiveScene().name == "Level3_Game"){
            hotSpot =  new Vector2(mouseTexture.width*0.5f, mouseTexture.height*0.5f);
        }
        else{
            hotSpot =  Vector2.zero;
        } 

        Cursor.SetCursor(mouseTexture, hotSpot, CursorMode.ForceSoftware);
    }
}
