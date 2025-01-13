using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_PlotManager : MonoBehaviour
{
    public static Level2_PlotManager Instance{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
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
        AudioManager.Instance.PlayMusic("Level2Plot");
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

    public void NextLevel(){
        GameManager.Instance.ChangeScene("Level2_Game");
    }

    public void ReturnMainPage(){
        GameManager.Instance.ChangeScene("MainPage");
    }
}
