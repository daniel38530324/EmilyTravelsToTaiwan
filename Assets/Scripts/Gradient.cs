using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gradient : MonoBehaviour
{
    public void ChangeBackground(){
        if(SceneManager.GetActiveScene().name == "Level3_Plot"){
            Level3_PlotManager.Instance.ChangeBackground();
        }
        else if(SceneManager.GetActiveScene().name == "Level5_Plot"){
            Level5_PlotManager.Instance.ChangeBackground();
        }
    }
}
