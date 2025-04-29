using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum Level6_GameState{
    Explain,
    Game,
    Success,
    Fail
}

public class Level6_GameManager : MonoBehaviour
{
    public static Level6_GameManager Instance{get; private set;}
    public Level6_GameState Level6_GameState{get; private set;}
    public bool PlayerCanMove{get; set;}

    //[field: SerializeField] public

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, explain_Button, return_Button, exit_Button, good_Image, bad_Image;
    [SerializeField] private GameObject[] questions;

    private int questionIndex;

    //[Header("Setting")]


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
        AudioManager.Instance.PlayMusic("Level1Game");
        UpdateLevel6_GameState(Level6_GameState.Explain);
    }

    public void UpdateLevel6_GameState(Level6_GameState newState){
        Level6_GameState = newState;

        switch(Level6_GameState){
            case Level6_GameState.Explain:
                  StartCoroutine(Explain());
                  PlayerCanMove = false;
                break;
            case Level6_GameState.Game:
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                PlayerCanMove = true;
                break;
            case Level6_GameState.Success:
                PlayerCanMove = false;
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                questions[0].transform.parent.gameObject.SetActive(false);
                success_Image.SetActive(true);
                break;
            case Level6_GameState.Fail:
                PlayerCanMove = false;
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                questions[0].transform.parent.gameObject.SetActive(false);
                fail_Image.SetActive(true);
                break;
        }
    }
    public void UpdateLevel6_GameState_Int(int newState){
        UpdateLevel6_GameState((Level6_GameState)newState);
    }

    public void ChangePlayerMove(bool result){
        PlayerCanMove = result;
    }

    public void OpenQuestion(int index){
        PlayerCanMove = false;
        questionIndex = index;
        questions[index].SetActive(true);
    }

    public void CheckQuestion(bool isCorrect){
        if(isCorrect){
            good_Image.SetActive(true);
        }
        else{
            bad_Image.SetActive(true);
        }
        StartCoroutine(CloseQuestion(isCorrect));
    }

    IEnumerator CloseQuestion(bool isCorrect){
        EventTrigger[] eventTriggers = questions[questionIndex].GetComponentsInChildren<EventTrigger>();
        if(isCorrect){
            foreach(EventTrigger item in eventTriggers){
                item.enabled = false;
            }
            yield return new WaitForSeconds(2);
            questions[questionIndex].GetComponent<Animator>().SetTrigger("Close");
        }
        else{
            foreach(EventTrigger item in eventTriggers){
                item.enabled = false;
            }
            yield return new WaitForSeconds(2);
            foreach(EventTrigger item in eventTriggers){
                item.enabled = true;
            }
        }
    }

    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CloseExplain(){
        if(Level6_GameState == Level6_GameState.Explain){
            UpdateLevel6_GameState(Level6_GameState.Game);
        }
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
