using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5_PlotManager : MonoBehaviour
{
    public static Level5_PlotManager Instance{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject gradient, background1, background2;
    [SerializeField] GameObject[] clickPoints;
    [SerializeField] GameObject[] wordAndGrammars;
    [SerializeField] GameObject[] details;

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
        AudioManager.Instance.PlayMusic("Level5Plot");
    }

    public void SetClickPointActive(int index){
        clickPoints[index].SetActive(true);
    }

    public void SetWordAndGrammarActive(int index){
        wordAndGrammars[index].SetActive(true);
    }

    public void CloseAnyClickPoint(){
        foreach(GameObject item in clickPoints){
            item.SetActive(false);
        }
    }

    public void CloseAnyDetail(){
        foreach(GameObject item in details){
            item.SetActive(false);
        }
    }

    public void ActiveGradient(){
        gradient.SetActive(true);
    }

    public void ChangeBackground(){
        background1.SetActive(false);
        background2.SetActive(true);
        AudioManager.Instance.StopAll();
        AudioManager.Instance.PlayMusic("Level5Plot2");
    }

    public void NextLevel(){
        GameManager.Instance.ChangeScene("Level1_Game");
    }

    public void ReturnMainPage(){
        GameManager.Instance.ChangeScene("MainPage");
    }
}
