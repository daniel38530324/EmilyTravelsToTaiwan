using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageManager : MonoBehaviour
{
    public static MainPageManager Instance{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject emily;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }

        if(GameManager.Instance == null){
            Instantiate(gameManager);
        }
    }

    private void Start() {
        AudioManager.Instance.PlayMusic("MainPage");
    }

    public void DisplayEmily(bool isDisplay){
        StartCoroutine(EmilyDely(isDisplay));
    }

    IEnumerator EmilyDely(bool isDisplay){   
        if(!isDisplay){
            for(int i = 0; i < 4; i++){    
                emily.transform.GetChild(i).gameObject.SetActive(isDisplay);
            }
        }
        else{
            yield return new WaitForSeconds(0.9f);
            for(int i = 0; i < 4; i++){    
                emily.transform.GetChild(i).gameObject.SetActive(isDisplay);
            }
        }
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }

    public void Exit(){
        GameManager.Instance.Exit();
    }
}
