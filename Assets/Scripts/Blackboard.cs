using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public void CloseBlackboard(){
        Level6_GameManager.Instance.PlayerCanMove = true;
        gameObject.SetActive(false);
    }
}
