using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodBless : MonoBehaviour
{
    public void OpenGodBless(){
        Level4_GameManager.Instance.SetOption(false);
    }
    
    public void CloseGodBless(){
        Level4_GameManager.Instance.SetOption(true);
        transform.parent.gameObject.SetActive(false);
    }
}
