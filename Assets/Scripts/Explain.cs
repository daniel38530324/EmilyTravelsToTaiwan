using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explain : MonoBehaviour
{
    public void CloseExplainAnimation(){
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void CloseExplain(){
        gameObject.SetActive(false);
    }
}
