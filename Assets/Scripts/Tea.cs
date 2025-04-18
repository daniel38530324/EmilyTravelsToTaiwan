using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea : MonoBehaviour
{
    public void StartAnimation(){
        Level5_GameManager.Instance.TeaAnimation(true);
    }

    public void EndAnimation(){
        Level5_GameManager.Instance.TeaAnimation(false);
    }

    public void HintAnimation(){
        Level5_GameManager.Instance.HintAnimation();
    }
}
