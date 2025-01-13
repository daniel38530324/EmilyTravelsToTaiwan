using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hint : MonoBehaviour
{
    public void CloseHint(){
        if(SceneManager.GetActiveScene().name == "Level3_Game"){
            Level3_GameManager.Instance.ClearTargetText();
        }
        else if(SceneManager.GetActiveScene().name == "Level4_Game"){
            Level4_GameManager.Instance.CloseQuestion();
            Level4_GameManager.Instance.UpdateLevel4_GameState(Level4_GameState.Mora);
        }

        gameObject.SetActive(false);
    }
}
