using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Level4_GameState{
    Explain,
    Mora,
    MoraFinal,
    Question,
    Success,
    Fail
}

public class Level4_GameManager : MonoBehaviour
{
    public static Level4_GameManager Instance{get; private set;}
    public Level4_GameState Level4_GameState{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, mora_Image, moraSelf, moraOther, question_Image, good_Image, bad_Image, explain_Button, return_Button, exit_Button, emilyWord;
    [SerializeField] private TMP_Text score_Text, fail_Text;
    [SerializeField] private GameObject[] moraDisplays_Self, moraDisplays_Other, questions, gods, godBless, mistakeOptions;

    private int questionIndex, moraIndex, godIndex, previousGodIndex, score;
    private float moraTimer;
    private bool canClick;

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
        AudioManager.Instance.PlayMusic("Level4Game");
        UpdateLevel4_GameState(Level4_GameState.Explain);
    }

    private void Update() {
        if(Level4_GameState == Level4_GameState.Mora){
            int previousMoraIndex;
            moraTimer += Time.deltaTime;
            if(moraTimer > 0.1f){
                moraTimer = 0;
                previousMoraIndex = moraIndex;
                moraIndex++;
                moraIndex = moraIndex > 2 ? 0 : moraIndex;
                //previousMoraIndex = moraIndex - 1 < 0 ? 2 : moraIndex - 1;
                // Debug.Log("moraIndex " + moraIndex);
                // Debug.Log("previousMoraIndex " + previousMoraIndex);
                moraDisplays_Self[previousMoraIndex].SetActive(false);
                moraDisplays_Self[moraIndex].SetActive(true);
                moraDisplays_Other[previousMoraIndex].SetActive(false);
                moraDisplays_Other[moraIndex].SetActive(true);
            }
        }
    }

    public void UpdateLevel4_GameState(Level4_GameState newState){
        Level4_GameState = newState;

        switch(Level4_GameState)
        {
            case Level4_GameState.Explain:
                StartCoroutine(Explain());
                break;
            case Level4_GameState.Mora:
                canClick = true;
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                question_Image.SetActive(false);
                moraSelf.SetActive(true);
                moraOther.SetActive(true);
                mora_Image.SetActive(true);
                gods[godIndex].SetActive(true);
                emilyWord.SetActive(true);
                score_Text.transform.parent.gameObject.SetActive(true);
                break;
            case Level4_GameState.MoraFinal:
                break;
            case Level4_GameState.Question:
                canClick = true;
                moraSelf.SetActive(false);
                moraOther.SetActive(false);
                mora_Image.SetActive(false);
                question_Image.SetActive(true);
                emilyWord.SetActive(false);
                gods[previousGodIndex].SetActive(false);
                questions[questionIndex].SetActive(true);
                break;
            case Level4_GameState.Success:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                question_Image.SetActive(false);
                success_Image.SetActive(true);
                break;
            case Level4_GameState.Fail:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                question_Image.SetActive(false);
                fail_Text.text = "請正確回答問題!";
                fail_Image.SetActive(true);
                break;
        }
    }

    public void UpdateLevel4_GameState_Int(int newState){
        UpdateLevel4_GameState((Level4_GameState)newState);
    }

    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CloseExplain(){
        if(Level4_GameState == Level4_GameState.Explain){
            UpdateLevel4_GameState(Level4_GameState.Mora);
        }
    }

    public void CheckMora(int mora){
        if(!canClick) {return;}
        canClick = false;

        UpdateLevel4_GameState(Level4_GameState.MoraFinal);
        foreach (GameObject item in moraDisplays_Self)
        {
            int index = Array.IndexOf(moraDisplays_Self, item);
            if(index == mora){
                item.SetActive(true);
            }
            else{
                item.SetActive(false);
            }
        }

        int otherMora = UnityEngine.Random.Range(0,3);
        foreach (GameObject item in moraDisplays_Other)
        {
            int index = Array.IndexOf(moraDisplays_Other, item);
            if(index == otherMora){
                item.SetActive(true);
            }
            else{
                item.SetActive(false);
            }
        }

        if(mora == otherMora){
            StartCoroutine(Mora(Level4_GameState.Mora, 2));
        }
        else if((mora == 0 && otherMora == 2) || (mora == 1 && otherMora == 0) || (mora == 2 && otherMora == 1)){
            previousGodIndex = godIndex;
            godIndex++;
            godIndex = godIndex > 3 ? 0 : godIndex;
            StartCoroutine(Mora(Level4_GameState.Question, 3.5f));
            StartCoroutine(SetGodBless());
        }
        else{
            previousGodIndex = godIndex;
            godIndex++;
            godIndex = godIndex > 3 ? 0 : godIndex;
            StartCoroutine(Mora(Level4_GameState.Question, 2));
        }
    }

    IEnumerator Mora(Level4_GameState targetState, float timer){
        yield return new WaitForSeconds(timer);
        UpdateLevel4_GameState(targetState);
    }

    public void CheckOption(bool isTrue){
        if(!canClick) {return;}
        canClick = false;

        questionIndex++;
        
        if(isTrue){
            score++;
            score_Text.text = $"X {score}";
            if(score >= 5){
                UpdateLevel4_GameState(Level4_GameState.Success);
                return;
            }

            if(questionIndex > 9){
                UpdateLevel4_GameState(Level4_GameState.Fail);
                return;
            }
            AudioManager.Instance.PlaySound("Correct");
            good_Image.SetActive(true);
        }
        else{
            if(questionIndex > 9){
                UpdateLevel4_GameState(Level4_GameState.Fail);
                return;
            }
            AudioManager.Instance.PlaySound("Error");
            bad_Image.SetActive(true);
        }

        //questions[questionIndex - 1].SetActive(false);
        
        //UpdateLevel4_GameState(Level4_GameState.Mora);
    }

    public void CloseQuestion(){
        questions[questionIndex - 1].SetActive(false);
    }

    public void SetOption(bool isOpen){
        Transform parent = mistakeOptions[questionIndex].transform.parent.transform;
        
        if(!isOpen){
            mistakeOptions[questionIndex].GetComponent<Button>().interactable = false;
            mistakeOptions[questionIndex].transform.GetChild(1).gameObject.SetActive(true);
        }

        for(int i = 1; i < 5; i++){
            if(parent.GetChild(i).name == mistakeOptions[questionIndex].name){
                continue;
            }
            parent.GetChild(i).GetComponent<Button>().interactable = isOpen;
        }
    }

    IEnumerator SetGodBless(){
        yield return new WaitForSeconds(2);
        AudioManager.Instance.PlaySound("GodBless");
        godBless[previousGodIndex].SetActive(true);
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
